import { Modal, Table } from "antd";
import axios from "axios";
import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";
import { toast } from "react-toastify";

const ProductsAdmin = observer(() => {

    const [postModalVisible, setPostModalVisible] = useState(false);
    const [updateModalVisible, setUpdateModalVisible] = useState(false);
    const [productPostModel, setProductPostModel] = useState({ text: '', type: '', base64: '', price: '' });
    const [productUpdateModel, setProductUpdateModel] = useState({ id: '', text: '', type: '', imagePath: '', price: '' });
    const [products, setProducts] = useState([]);

    const columns = [
        {
            title: 'Id',
            dataIndex: 'id',
            key: 'id'
        },
        {
            title: 'ImagePath',
            dataIndex: 'imagePath',
            key: 'imagePath'
        },
        {
            title: 'Text',
            dataIndex: 'text',
            key: 'text'
        },
        {
            title: 'Price',
            dataIndex: 'price',
            key: 'price'
        },
        {
            title: 'Actions',
            key: 'actions',
            render: (text, record) => (
                <div className="p-2">
                    <button onClick={() => {
                        setProductUpdateModel(record);
                        setUpdateModalVisible(true);
                    }} className="bg-amber-500 text-white px-3 py-1 rounded mr-2">Update</button>
                    <button onClick={() => deleteProduct(record.id)} className="bg-red-500 text-white px-3 py-1 rounded">Delete</button>
                </div>
            )
        }
    ];

    useEffect(() => {
        axios.get('https://localhost:7146/api/products').then(response => {
            setProducts(response.data);
        });
    }, []);

    const postProduct = () => {
        axios.post('https://localhost:7146/api/product', productPostModel).then(response => {
            axios.get('https://localhost:7146/api/products').then(response => {
                setProducts(response.data);
            });
            toast.success('Product added succesfully');
            setPostModalVisible(false);
            setProductPostModel({ text: '', type: '', base64: '', price: '' });
        }).catch(error => {
            toast.error('Server error');
        });
    }

    const updateProduct = product => {
        axios.put('https://localhost:7146/api/product', productUpdateModel).then(response => {
            axios.get('https://localhost:7146/api/products').then(response => {
                setProducts(response.data);
            });
            toast.success('Product updated succesfully');
            setProductUpdateModel({ id: '', text: '', type: '', imagePath: '', price: '' });
            setUpdateModalVisible(false);
        }).catch(error => {
            toast.error('Server error');
        });
    }

    const deleteProduct = productId => {
        axios.delete('https://localhost:7146/api/product/' + productId).then(response => {
            axios.get('https://localhost:7146/api/products').then(response => {
                setProducts(response.data);
            });
            toast.success('Deleted succesfully');
        }).catch(error => {
            toast.error('Server error');
        });
    }

    const getBase64 = e => {
        const reader = new FileReader();
        reader.readAsDataURL(e.target.files[0]);
        reader.onload = function () {
            console.log(reader.result);
            setProductPostModel({ ...productPostModel, base64: reader.result });
            document.getElementById('file-input').value = null;
        };
        reader.onerror = function (error) {
            console.log('Error: ', error);
        };
    }

    return(
        <div className="products-admin">
            <Modal visible={updateModalVisible} onOk={updateProduct} onCancel={() => setUpdateModalVisible(false)}>
                <div className="px-5 py-3">
                    <div className="mb-4 text-xl">
                        Update product
                    </div>
                    <div className="mb-4">
                        <input className="mx-auto shadow appearance-none border rounded w-3/4 py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                               type="text" 
                               value={productUpdateModel.text} 
                               placeholder="Text content" 
                               onChange={e => setProductUpdateModel({ ...productUpdateModel, text: e.target.value })}
                        />
                    </div>
                    <div className="mb-4">
                        <input className="mx-auto shadow appearance-none border rounded w-3/4 py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                               type="text" 
                               value={productUpdateModel.type} 
                               placeholder="Product type" 
                               onChange={e => setProductUpdateModel({ ...productUpdateModel, type: e.target.value })}
                        />
                    </div>
                    <div className="mb-4">
                        <input className="mx-auto shadow appearance-none border rounded w-3/4 py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                               type="number" 
                               value={productUpdateModel.price} 
                               placeholder="Product price" 
                               onChange={e => setProductUpdateModel({ ...productUpdateModel, price: e.target.value })}
                        />
                    </div>
                </div>
            </Modal>
            <Modal visible={postModalVisible} onOk={postProduct} onCancel={() => setPostModalVisible(false)}>
                <div className="px-5 py-3">
                    <div className="mb-4">
                        <input className="mx-auto shadow appearance-none border rounded w-3/4 py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                               type="text" 
                               value={productPostModel.text} 
                               placeholder="Text content" 
                               onChange={e => setProductPostModel({ ...productPostModel, text: e.target.value })}
                        />
                    </div>
                    <div className="mb-4">
                        <img className="w-1/2" src={productPostModel.base64}/>
                        <input className="mx-auto" 
                               id="file-input"
                               name="file" 
                               type="file" 
                               onChange={getBase64}
                        />
                    </div>
                    <div className="mb-4">
                        <input className="mx-auto shadow appearance-none border rounded w-3/4 py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                               type="text" 
                               value={productPostModel.type} 
                               placeholder="Product type" 
                               onChange={e => setProductPostModel({ ...productPostModel, type: e.target.value })}
                        />
                    </div>
                    <div className="mb-4">
                        <input className="mx-auto shadow appearance-none border rounded w-3/4 py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                               type="number" 
                               value={productPostModel.price} 
                               placeholder="Product price" 
                               onChange={e => setProductPostModel({ ...productPostModel, price: e.target.value })}
                        />
                    </div>
                </div>
            </Modal>

            <div className="container mx-auto pt-24">
                <button onClick={() => setPostModalVisible(true)} className="px-3 py-1 mb-4 bg-blue-500 text-white rounded">Add product</button>
                <Table columns={columns} dataSource={[...products]} pagination={false}/>
            </div>
        </div>
    );
});

export default ProductsAdmin;