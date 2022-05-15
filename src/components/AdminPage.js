import {Drawer, Modal, Table} from "antd";
import axios from "axios";
import { observer } from "mobx-react-lite";
import {useEffect, useState} from "react";
import { useNavigate } from "react-router-dom";

const AdminPage = observer(() => {
    const [drawerVisible, setDrawerVisible] = useState(false);
    const [isModalVisible, setIsModalVisible] = useState(false);
    const [manager, setManager] = useState({ email: '', password: ''});
    const [favorOrders, setFavorOrders] = useState([]);
    const [productOrders, setProductOrders] = useState([]);
    const navigate = useNavigate();

    const favorOrderColumns = [
        {
            title: 'UserID',
            dataIndex: 'userId',
            key: 'favorOrderUserId'
        },
        {
            title: 'FavorId',
            dataIndex: 'favorId',
            key: 'favorOrderFavorId'
        },
        {
            title: 'Created',
            dataIndex: 'created',
            key: 'favorOrderCreated',
            render: (text, record) => (
                <div>
                    {text.substring(0, text.length - 8).replace('T', ' ')}
                </div>
            )
        },
        {
            title: 'TotalPrice',
            dataIndex: 'totalPrice',
            key: 'favorOrderTotalPrice',
            render: (text, record) => (
                <div>
                    {text} $
                </div>
            )
        },
        {
            title: 'Priority',
            dataIndex: 'isFastOrder',
            key: 'favorOrderIsFastOrder',
            render: (text, record) => (
                <div>
                    {
                        record.isFastOrder ?
                            <div className="bg-green-500 text-center text-white p-1 rounded">Important</div>
                            :
                            <div className="bg-yellow-500 text-center text-white p-1 rounded">Usual</div>
                    }
                </div>
            )
        }
    ];

    const productOrderColumns = [
        {
            title: 'UserID',
            dataIndex: 'userId',
            key: 'favorOrderUserId'
        },
        {
            title: 'ProductID',
            dataIndex: 'productId',
            key: 'productOrderProductId'
        },
        {
            title: 'Created',
            dataIndex: 'created',
            key: 'productOrderCreated',
            render: (text, record) => (
                <div>
                    {text.substring(0, text.length - 8).replace('T', ' ')}
                </div>
            )
        },
        {
            title: 'TotalPrice',
            dataIndex: 'totalPrice',
            key: 'productOrderTotalPrice',
            render: (text, record) => (
                <div>
                    {text} $
                </div>
            )
        }
    ];

    useEffect(() => {
        axios.get('https://localhost:7146/api/favor/order').then(response => {
            setFavorOrders(response.data);
        });
        axios.get('https://localhost:7146/api/product/order').then(response => {
           setProductOrders(response.data);
        });
    }, []);

    const createManager = () => {
        axios.post('https://localhost:7146/api/admin/manager').then(response => {
            setManager(response.data);
            setIsModalVisible(true);
        });
    }

    return(
        <div className="admin-page min-h-screen bg-gray-200 pt-12">
            <Drawer title="Advertising agency | admin menu" placement="left" onClose={() => setDrawerVisible(false)} visible={drawerVisible}>
                <div className="p-3 grid grid-cols-1 gap-y-5">
                    <button onClick={createManager} className="bg-blue-500 px-3 py-2 text-center text-white rounded">Generate manager account</button>
                    <button onClick={() => navigate('/admin/products')} className="bg-amber-600 px-3 py-2 text-center text-white rounded">Products panel</button>
                    <button onClick={() => navigate('/admin/favors')} className="bg-indigo-800 px-3 py-2 text-center text-white rounded">Favors panel</button>
                </div>
            </Drawer>

            <div className="container mx-auto text-center pt-24">
                <button onClick={() => setDrawerVisible(true)} className="mx-auto mb-4 bg-blue-700 px-3 py-2 text-center text-3xl text-white rounded">
                    Open admin`s menu
                </button>
                <div className="text-black text-left text-2xl mb-2">Favor orders:</div>
                <Table pagination={false} columns={favorOrderColumns} dataSource={[...favorOrders]}/>
                <div className="text-black text-left text-2xl my-2">Product orders:</div>
                <Table pagination={false} columns={productOrderColumns} dataSource={[...productOrders]}/>
            </div>
           
            <Modal visible={isModalVisible} onOk={() => setIsModalVisible(false)} onCancel={() => setIsModalVisible(false)}>
                <div className="px-5 py-3 grid grid-cols-1">
                    <div className="text-black text-lg">Manager email: {manager.email}</div>
                    <div className="text-black text-lg">Manager password: {manager.password}</div>
                </div>
            </Modal>
        </div>
    );
});

export default AdminPage;