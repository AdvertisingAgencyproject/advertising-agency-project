import { Modal } from "antd";
import axios from "axios";
import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";
import { toast } from "react-toastify";
import authStore from "../stores/auth.store";
import Favor from "./Favor";
import { Radio } from 'antd';

const FavorList = observer(() => {
    const [favors, setfavors] = useState([]);
    const [favorOrderModel, setfavorOrderModel] = useState({ favorId: '', userId: ''});
    const [isModalVisible, setIsModalVisible] = useState(false);
    const [fastOrder, setFastOrder] = useState(false);

    useEffect(() => {
        axios.get('https://localhost:7146/api/favors').then(response => {
            setfavors(response.data);
        });
    }, []);

    const openModal = favorId => {
        setfavorOrderModel({ favorId, userId : authStore.getUserId() });
        setIsModalVisible(true);
    }

    const orderFavor = () => {
        axios.post('https://localhost:7146/api/favor/order', {...favorOrderModel, isFastOrder: fastOrder}).then(response => {
            setfavorOrderModel({ favorId: '', userId: '' });
            setIsModalVisible(false);
            toast.success('Ordered succesfully');
        }).catch(error => {
            toast.error('Server error');
        });
    }

    const handleFastOrderChange = e => {
        setFastOrder(e.target.value);
    }

    return(
        <div className="favor-list min-h-screen bg-gray-200">
            <div className="favor-list pt-24 grid grid-cols-4 gap-10 container mx-auto">
            <Modal visible={isModalVisible} onOk={orderFavor} onCancel={() => setIsModalVisible(false)}>
                <div className="px-5 py-3 grid grid-cols-1">
                    <div className="text-black">Fast order (we will handle your order faster) + 5$</div>
                    <Radio.Group onChange={handleFastOrderChange} value={fastOrder}>
                        <Radio value={true}>Yes</Radio>
                        <Radio value={false}>No</Radio>
                    </Radio.Group>
                    <div className="my-4 text-xl">
                        Click ok to order
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
                               discountPercents={favor.discountPercents}
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