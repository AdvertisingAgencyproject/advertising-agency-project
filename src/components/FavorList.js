import { Modal } from "antd";
import axios from "axios";
import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";
import { toast } from "react-toastify";
import authStore from "../stores/auth.store";
import Favor from "./Favor";

const FavorList = observer(() => {
    const [favors, setfavors] = useState([]);
    const [favorOrderModel, setfavorOrderModel] = useState({ favorId: '', userId: ''});
    const [isModalVisible, setIsModalVisible] = useState(false);
    const [selectedfavorPrice, setSelectedfavorPrice] = useState(0);

    useEffect(() => {
        axios.get('https://localhost:7146/api/favors').then(response => {
            setfavors(response.data);
        });
    }, []);

    const openModal = (favorId, favorPrice) => {
        setfavorOrderModel({ favorId, userId : authStore.getUserId() });
        setSelectedfavorPrice(favorPrice);
        setIsModalVisible(true);
    }

    const orderfavor = () => {
        axios.post('https://localhost:7146/api/favor/order', favorOrderModel).then(response => {
            setfavorOrderModel({ favorId: '', userId: '' });
            setIsModalVisible(false);
            toast.success('Ordered succesfully');
        }).catch(error => {
            toast.error('Server error');
        });
    }

    return(
        <div className="favor-list min-h-screen bg-gray-200">
            <div className="favor-list pt-24 grid grid-cols-4 gap-10 container mx-auto">
            <Modal visible={isModalVisible} onOk={orderfavor} onCancel={() => setIsModalVisible(false)}>
                <div className="px-5 py-3 grid grid-cols-1">
                    <div className="mb-4 text-black text-xl">Make order</div>
                    <div className="mb-4">
                        Total price: {selectedfavorPrice} $
                    </div>
                </div>
            </Modal>
            {
                favors.map((favor, index) => 
                    (
                        <Favor key={index} 
                               openModal={openModal}
                               id={favor.id}
                               price={favor.price} 
                               title={favor.text} 
                               type={favor.type} 
                               imagePath={favor.imagePath}
                        />
                    )
                )
            }
            </div>
        </div>
    );
});

export default FavorList;