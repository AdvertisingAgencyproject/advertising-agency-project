import { observer } from "mobx-react-lite";
import authStore from "../stores/auth.store";

const Product = observer(props => {
    return(
        <div className="product shadow-lg p-3 bg-white">
            <img className="mx-auto" alt="Image" src={'https://localhost:7146/' + props.imagePath}/>
            <div className="text-black text-2xl">{props.text}</div>
            <div className="text-black">{props.type}</div>
            <div className="grid grid-cols-2 items-center">
                <div className="text-black text-lg text-left font-bold">{props.price} $</div>
                { authStore.isAuthenticated && <button onClick={() => props.openModal(props.id, props.price)} className="bg-green-600 hover:bg-green-500 px-3 py-2 rounded shadow-md text-white float-right">Order</button> }
            </div>
        </div>
    );
});

export default Product;