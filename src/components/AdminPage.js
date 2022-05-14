import { Drawer, Modal } from "antd";
import axios from "axios";
import { observer } from "mobx-react-lite";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

const AdminPage = observer(() => {
    const [drawerVisible, setDrawerVisible] = useState(true);
    const [isModalVisible, setIsModalVisible] = useState(false);
    const [manager, setManager] = useState({ email: '', password: ''});
    const navigate = useNavigate();

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
            <div className="container mx-auto text-center pt-36">
                <button onClick={() => setDrawerVisible(true)} className="mx-auto bg-blue-700 px-3 py-2 text-center text-3xl text-white rounded">Open admin`s menu</button>
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