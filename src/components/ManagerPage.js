import { Drawer, Modal } from "antd";
import axios from "axios";
import { observer } from "mobx-react-lite";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

const ManagerPage = observer(() => {
    const [drawerVisible, setDrawerVisible] = useState(true);
    const navigate = useNavigate();

    return(
        <div className="manager-page min-h-screen bg-gray-200 pt-12">
            <Drawer title="Advertising agency | manager menu" placement="left" onClose={() => setDrawerVisible(false)} visible={drawerVisible}>
                <div className="p-3 grid grid-cols-1 gap-y-5">
                    <button onClick={() => navigate('/admin/products')} className="bg-amber-600 px-3 py-2 text-center text-white rounded">Products panel</button>
                    <button onClick={() => navigate('/admin/favors')} className="bg-indigo-800 px-3 py-2 text-center text-white rounded">Favors panel</button>
                </div>
            </Drawer>
            <div className="container mx-auto text-center pt-36">
                <button onClick={() => setDrawerVisible(true)} className="mx-auto bg-blue-700 px-3 py-2 text-center text-3xl text-white rounded">Open manager`s menu</button>
            </div>
        </div>
    );
}); 

export default ManagerPage;