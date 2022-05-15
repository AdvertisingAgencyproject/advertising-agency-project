import { Modal, Table } from "antd";
import axios from "axios";
import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";
import { toast } from "react-toastify";
import favor from "./Favor";

const FavorsAdmin = observer(() => {

    const [discountModalVisible, setDiscountModalVisible] = useState(false);
    const [discountModel, setDiscountModel] = useState({ favorId: '', percents: ''});
    const [postModalVisible, setPostModalVisible] = useState(false);
    const [updateModalVisible, setUpdateModalVisible] = useState(false);
    const [favorPostModel, setfavorPostModel] = useState({ title: '', type: '', base64: '', price: 0, isFastOrder: false});
    const [favorUpdateModel, setfavorUpdateModel] = useState({ id: '', title: '', type: '', imagePath: '', price: ''});
    const [favors, setfavors] = useState([]);

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
            title: 'Title',
            dataIndex: 'title',
            key: 'title'
        },
        {
            title: 'Price',
            dataIndex: 'price',
            key: 'price'
        },
        {
            title: 'Type',
            dataIndex: 'type',
            key: 'type'
        },
        {
            title: 'Actions',
            key: 'actions',
            render: (text, record) => (
                <div className="p-2">
                    <button onClick={() => {
                        setfavorUpdateModel(record);
                        setUpdateModalVisible(true);
                    }} className="bg-amber-500 text-white px-3 py-1 rounded mr-2">Update</button>
                    <button onClick={() => deletefavor(record.id)} className="bg-red-500 text-white px-3 py-1 rounded mr-2">Delete</button>
                    { record.discountPercents === null && <button onClick={() => openDiscountAddModal(record.id)} className="bg-green-500 text-white px-3 py-1 rounded">Set discount</button> }
                </div>
            )
        }
    ];

    useEffect(() => {
        axios.get('https://localhost:7146/api/favors').then(response => {
            setfavors(response.data);
        });
    }, []);

    const postfavor = () => {
        var validationError = false;

        if(favorPostModel.title === ''){
            toast.error('Title is required!');
            validationError = true;
        }
        if(favorPostModel.type === ''){
            toast.error('Type is required!');
            validationError = true;
        }
        if(favorPostModel.price === 0 || favorUpdateModel.price === ''){
            toast.error('Price should be greater than 0!');
            validationError = true;
        }
        if(favorPostModel.base64 === ''){
            toast.error('Upload an image. That is required!');
            validationError = true;
        }

        if(validationError){
            return;
        }

        axios.post('https://localhost:7146/api/favor', favorPostModel).then(response => {
            axios.get('https://localhost:7146/api/favors').then(response => {
                setfavors(response.data);
            });
            toast.success('favor added succesfully');
            setPostModalVisible(false);
            setfavorPostModel({ title: '', type: '', base64: '', price: '' });
        }).catch(error => {
            toast.error('Server error');
        });
    }

    const updatefavor = favor => {

        var validationError = false;

        if(favorUpdateModel.title === ''){
            toast.error('Title is required!');
            validationError = true;
        }
        if(favorUpdateModel.type === ''){
            toast.error('Type is required!');
            validationError = true;
        }
        if(favorUpdateModel.price === 0 || favorUpdateModel.price === ''){
            toast.error('Price should be greater than 0!');
            validationError = true;
        }

        if(validationError){
            return;
        }

        axios.put('https://localhost:7146/api/favor', favorUpdateModel).then(response => {
            axios.get('https://localhost:7146/api/favors').then(response => {
                setfavors(response.data);
            });
            toast.success('favor updated succesfully');
            setfavorUpdateModel({ id: '', title: '', type: '', imagePath: '', price: '' });
            setUpdateModalVisible(false);
        }).catch(error => {
            toast.error('Server error');
        });
    }

    const deletefavor = favorId => {
        axios.delete('https://localhost:7146/api/favor/' + favorId).then(response => {
            axios.get('https://localhost:7146/api/favors').then(response => {
                setfavors(response.data);
            });
            toast.success('Deleted succesfully');
        }).catch(error => {
            toast.error('Server error');
        });
    }

    const openDiscountAddModal = favorId => {
        setDiscountModel({...discountModel, favorId });
        setDiscountModalVisible(true);
    }

    const addDiscountToFavor = () => {
        axios.post('https://localhost:7146/api/favor/discount', discountModel).then(response => {
            axios.get('https://localhost:7146/api/favors').then(response => {
                setfavors(response.data);
            });
        });
        setDiscountModalVisible(false);
        toast.success('Discount created succesfully');
    }

    const getBase64 = e => {
        const reader = new FileReader();
        reader.readAsDataURL(e.target.files[0]);
        reader.onload = function () {
            console.log(reader.result);
            setfavorPostModel({ ...favorPostModel, base64: reader.result });
            document.getElementById('file-input').value = null;
        };
        reader.onerror = function (error) {
            console.log('Error: ', error);
        };
    }

    return(
        <div className="favors-admin">
            <Modal visible={updateModalVisible} onOk={updatefavor} onCancel={() => setUpdateModalVisible(false)}>
                <div className="px-5 py-3">
                    <div className="mb-4 text-xl">
                        Update favor
                    </div>
                    <div className="mb-4">
                        <input className="mx-auto shadow appearance-none border rounded w-3/4 py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                               type="text" 
                               value={favorUpdateModel.title} 
                               placeholder="Title" 
                               onChange={e => setfavorUpdateModel({ ...favorUpdateModel, title: e.target.value })}
                        />
                    </div>
                    <div className="mb-4">
                        <input className="mx-auto shadow appearance-none border rounded w-3/4 py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                               type="text" 
                               value={favorUpdateModel.type} 
                               placeholder="Favor type" 
                               onChange={e => setfavorUpdateModel({ ...favorUpdateModel, type: e.target.value })}
                        />
                    </div>
                    <div className="mb-4">
                        <input className="mx-auto shadow appearance-none border rounded w-3/4 py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                               type="number" 
                               value={favorUpdateModel.price} 
                               placeholder="Favor price" 
                               onChange={e => setfavorUpdateModel({ ...favorUpdateModel, price: e.target.value })}
                        />
                    </div>
                </div>
            </Modal>
            <Modal visible={postModalVisible} onOk={postfavor} onCancel={() => setPostModalVisible(false)}>
                <div className="px-5 py-3">
                    <div className="mb-4 text-black text-xl">Add favor</div>
                    <div className="mb-4">
                        <input className="mx-auto shadow appearance-none border rounded py-2 px-3 w-3/4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                               type="text" 
                               value={favorPostModel.title} 
                               placeholder="Title" 
                               onChange={e => setfavorPostModel({ ...favorPostModel, title: e.target.value })}
                        />
                    </div>
                    <div className="mb-4">
                        <img className="w-1/2" src={favorPostModel.base64}/>
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
                               value={favorPostModel.type} 
                               placeholder="Favor type" 
                               onChange={e => setfavorPostModel({ ...favorPostModel, type: e.target.value })}
                        />
                    </div>
                    <div className="mb-4">
                        <input className="mx-auto shadow appearance-none border rounded w-3/4 py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                               type="number" 
                               value={favorPostModel.price} 
                               placeholder="Favor price" 
                               onChange={e => setfavorPostModel({ ...favorPostModel, price: e.target.value })}
                        />
                    </div>
                </div>
            </Modal>
            <Modal visible={discountModalVisible} onOk={addDiscountToFavor} onCancel={() => setDiscountModalVisible(false)}>
                <div className="px-5 py-3">
                    <input type="number" placeholder="Percents" value={discountModel.percents} onChange={e => setDiscountModel({...discountModel, percents: e.target.value})}/>
                </div>
            </Modal>

            <div className="container mx-auto pt-24">
                <button onClick={() => setPostModalVisible(true)} className="px-3 py-1 mb-4 bg-blue-500 text-white rounded">Add favor</button>
                <Table columns={columns} dataSource={[...favors]} pagination={false}/>
            </div>
        </div>
    );
});

export default FavorsAdmin;