import axios from "axios";
import { observer } from "mobx-react-lite";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { toast } from 'react-toastify';
import authStore from "../stores/auth.store";

const Register = observer(() => {
    const [model, setModel] = useState({ email: '', password: '', fullName: '' });
    const navigate = useNavigate();

    const register = () => {
        axios.post('https://localhost:7146/api/register', model).then(response => {
            localStorage.setItem('token', response.data.token);
            authStore.setIsAuthenticated(true);
            if(authStore.isAdmin()){
                navigate('/admin');
            }else{
                navigate('/');
            }
            toast.success('Successfully registered and logged in');
        }).catch(error => {
            console.log("Error here");
            toast.error('Wrong data');
        });
    }

    return(
        <div className="register min-h-screen">
            <div className="container mx-auto pt-36">
                <div className="w-1/4 rounded-lg shadow-lg py-3 mx-auto grid grid-cols-1">
                    <h2 className="text-center mb-4 font-semibold text-2xl">Sign up</h2>
                    <input className="mx-auto shadow appearance-none border rounded w-3/4 py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4" 
                           type="text" 
                           placeholder="Email"
                           value={model.email} 
                           onChange={e => setModel({...model, email: e.target.value})}
                    />
                    <input className="mx-auto shadow appearance-none border rounded w-3/4 py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4" 
                           type="password" 
                           placeholder="Password"
                           value={model.password} 
                           onChange={e => setModel({...model, password: e.target.value})}
                    />
                    <input className="mx-auto shadow appearance-none border rounded w-3/4 py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4" 
                           type="text" 
                           placeholder="Fullname"
                           value={model.fullName} 
                           onChange={e => setModel({...model, fullName: e.target.value})}
                    />
                    <button onClick={register} className="bg-blue-500 text-white rounded w-1/3 mx-auto text-center">Submit</button>
                </div>
            </div>
        </div>
    );
});

export default Register;