import { Modal } from "antd";
import axios from "axios";
import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";
import { toast } from "react-toastify";
import authStore from "../stores/auth.store";
import Product from "./Product";

const ProductList = observer(() => {
    const [products, setProducts] = useState([]);
    const [productOrderModel, setProductOrderModel] = useState({ productId: '', userId: '', text: '', count: ''});
    const [isModalVisible, setIsModalVisible] = useState(false);
    const [selectedProductPrice, setSelectedProductPrice] = useState(0);

    useEffect(() => {
        axios.get('https://localhost:7146/api/products').then(response => {
            setProducts(response.data);
        });
    }, []);

    const openModal = (productId, productPrice) => {
        setProductOrderModel({ ...productOrderModel, productId, userId : authStore.getUserId() });
        setSelectedProductPrice(productPrice);
        setIsModalVisible(true);
    }

    const orderProduct = () => {
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