import {Modal, Select, Slider} from "antd";
import axios from "axios";
import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";
import { toast } from "react-toastify";
import authStore from "../stores/auth.store";
import Product from "./Product";

const ProductList = observer(() => {
    const { Option } = Select;
    const [products, setProducts] = useState([]);
    const [productOrderModel, setProductOrderModel] = useState({ productId: '', userId: '', text: '', count: ''});
    const [isModalVisible, setIsModalVisible] = useState(false);
    const [selectedProductPrice, setSelectedProductPrice] = useState(0)
    const [filters, setFilters] = useState();
    const [searchQuery, setSearchQuery] = useState('');
    const [typeFilter, setTypeFilter] = useState('all');
    const [selectedMinPrice, setSelectedMinPrice] = useState(0);
    const [selectedMaxPrice, setSelectedMaxPrice] = useState(0);
    const [selectedType, setSelectedType] = useState('all');
    const [types, setTypes] = useState([]);

    useEffect(() => {
        axios.get('https://localhost:7146/api/product/filter').then(response => {
            setFilters(response.data);
            setTypes(response.data.types);
            setSelectedMaxPrice(response.data.maxPrice);
            setSelectedMinPrice(response.data.minPrice);
        });
        axios.get('https://localhost:7146/api/products/all').then(response => {
            setProducts(response.data);
        });
    }, []);

    const handleSearch = () => {
        var search = '%default%';
        if(searchQuery !== ''){
            search = searchQuery;
        }
        axios.get(`https://localhost:7146/api/products/${search}/${selectedType}/${selectedMinPrice}/${selectedMaxPrice}`).then(response => {
            setProducts(response.data);
            console.log(response.data);
        });
    }

    const handleSearchInputChange = e => {
        var search = '%default%';
        if(e.target.value === ''){
            axios.get(`https://localhost:7146/api/products/${search}/${selectedType}/${selectedMinPrice}/${selectedMaxPrice}`).then(response => {
                setProducts(response.data);
            });
        }
        setSearchQuery(e.target.value);
    }

    const handleSelectedMaxPriceChange = e => {
        if(e.target.value = ''){
            setSelectedMaxPrice(filters.maxPrice);
        }
    }

    const handleSelectedMinPriceChange = e => {
        if(e.target.value = ''){
            setSelectedMinPrice(filters.minPrice);
        }
    }

    const handleTypeSelectChange = value => {
        setSelectedType(value);
    }

    const openModal = (productId, productPrice) => {
        setProductOrderModel({ ...productOrderModel, productId, userId : authStore.getUserId() });
        setSelectedProductPrice(productPrice);
        setIsModalVisible(true);
    }

    const orderProduct = () => {
        var validationError = false;

        if(productOrderModel.count === 0 || productOrderModel.count === ''){
            toast.error('Count should be greater than 0!');
            validationError = true;
        }

        if(productOrderModel.text === ''){
            toast.error('Wanted text is required!');
            validationError = true;
        }

        if(validationError){
            return;
        }

        axios.post('https://localhost:7146/api/product/order', productOrderModel).then(response => {
            setProductOrderModel({ productId: '', userId: '', text: '', count: '' });
            setIsModalVisible(false);
            toast.success('Ordered succesfully');
        }).catch(error => {
            toast.error('Server error');
        });
    }

    return(
        <div className="product-list min-h-screen bg-gray-200">
            <div className="pt-24 container mx-auto">
                <div className="block mb-10">
                    <input className="w-1/3 mb-4 shadow appearance-none border py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                           type="text"
                           value={searchQuery}
                           placeholder="Search"
                           onChange={handleSearchInputChange}
                    />
                    <button className="bg-green-600 text-white px-3 py-1" onClick={handleSearch}>Search & apply filters</button>
                </div>
                <div className="grid grid-cols-2 w-1/3">
                    <div className="text-black text-lg text-left">Min price:</div>
                    <input className="mb-4 shadow appearance-none border rounded py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                           type="number"
                           value={selectedMinPrice}
                           onChange={e => setSelectedMinPrice(e.target.value)}
                    />
                    <div className="text-black text-lg text-left">Max price:</div>
                    <input className="mb-4 shadow appearance-none border rounded py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                           type="number"
                           value={selectedMaxPrice}
                           onChange={e => setSelectedMaxPrice(e.target.value)}
                    />
                </div>
                <div className="grid grid-cols-2 items-center w-1/3">
                    <div className="text-black text-lg text-left">Polygraphy type:</div>
                    <Select style={{ width: 120 }} defaultValue="all" onChange={handleTypeSelectChange}>
                        <Option value="all">All</Option>
                        {
                            types.map((record, index) => (
                                <Option key={index} value={record}>
                                    {record}
                                </Option>
                            ))
                        }
                    </Select>
                </div>
            </div>
            <div className="product-list pt-24 grid grid-cols-4 gap-10 container mx-auto">
                <Modal visible={isModalVisible} onOk={orderProduct} onCancel={() => setIsModalVisible(false)}>
                    <div className="px-5 py-3 grid grid-cols-1">
                        <div className="mb-4 text-black text-xl">Make order</div>
                        <input className="mb-4 shadow appearance-none border rounded py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                               type="text"
                               placeholder="Wanted text"
                               value={productOrderModel.text}
                               onChange={e => setProductOrderModel({ ...productOrderModel, text: e.target.value })}
                        />
                        <input className="mb-4 shadow appearance-none border rounded py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                               type="number"
                               placeholder="Count"
                               value={productOrderModel.count}
                               onChange={e => setProductOrderModel({ ...productOrderModel, count: e.target.value })}
                        />
                        <div className="mb-4">
                            Total price: {selectedProductPrice * productOrderModel.count} $
                        </div>
                    </div>
                </Modal>
                {
                    products.map((product, index) =>
                        (
                            <Product key={index}
                                     openModal={openModal}
                                     id={product.id}
                                     price={product.price}
                                     text={product.text}
                                     type={product.type}
                                     imagePath={product.imagePath}
                            />
                        )
                    )
                }
            </div>
        </div>
    );
});

export default ProductList;