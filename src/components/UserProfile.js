import { Table } from "antd";
import axios from "axios";
import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";
import authStore from "../stores/auth.store";

const UserProfile = observer(() => {
    const [productOrders, setProductOrders] = useState([]);
    const [favorOrders, setFavorOrders] = useState([]);
    const productOrdersColumns = [
        {
            title: 'ID',
            dataIndex: 'id',
            key: 'id'
        },
        {
            title: 'UserID',
            dataIndex: 'userId',
            key: 'userId'
        },
        {
            title: 'ProductID',
            dataIndex: 'productId',
            key: 'productId'
        },
        {
            title: 'Wanted text',
            dataIndex: 'text',
            key: 'text'
        },
        {
            title: 'TotalPrice',
            dataIndex: 'totalPrice',
            key: 'totalPrice',
            render: ((record, text) => (
                <div>{record} $</div>
            ))
        },
        {
            title: 'Created',
            dataIndex: 'created',
            key: 'created'
        }
    ];

    const favorOrdersColumns = [
        {
            title: 'ID',
            dataIndex: 'id',
            key: 'id'
        },
        {
            title: 'UserID',
            dataIndex: 'userId',
            key: 'userId'
        },
        {
            title: 'FavorID',
            dataIndex: 'favorId',
            key: 'favorId'
        },
        {
            title: 'TotalPrice',
            dataIndex: 'totalPrice',
            key: 'totalPrice',
            render: ((record, text) => (
                <div>{record} $</div>
            ))
        },
        {
            title: 'Created',
            dataIndex: 'created',
            key: 'created'
        }
    ];

    useEffect(() => {
        const userId = authStore.getUserId();
        axios.get('https://localhost:7146/api/product/order/' + userId).then(response => {
            setProductOrders(response.data);
        });
        axios.get('https://localhost:7146/api/favor/order/' + userId).then(response => {
            setFavorOrders(response.data);
        });
    }, []);

    return(
        <div className="user-profile pt-24 min-h-screen bg-gray-200">
            <div className="container mx-auto">
                <div className="mb-4 text-black text-xl">My product orders:</div>
                <Table pagination={false} columns={productOrdersColumns} dataSource={[...productOrders]}/>
                <div className="my-4 text-black text-xl">My favor orders:</div>
                <Table pagination={false} columns={favorOrdersColumns} dataSource={[...favorOrders]}/>
            </div>
        </div>
    );
});

export default UserProfile;