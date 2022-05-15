import { observer } from "mobx-react-lite";
import { Link, useNavigate } from "react-router-dom";
import authStore from "../stores/auth.store";

const Nav = observer(() => {

    const navigate = useNavigate();

    const logout = () => {
        localStorage.clear();
        authStore.reset();
        navigate('/');
        window.location.reload();
    }

    return(
        <div className="nav absolute bg-cyan-900 w-screen p-2">
            <div className="container mx-auto grid grid-cols-10 items-center">
                { 
                    authStore.isAdmin()
                    && <Link to="/admin" className="col-span-3 text-white text-2xl">Advertising agency</Link>
                }
                {
                    authStore.isManager()
                    && <Link to="/manager" className="col-span-3 text-white text-2xl">Advertising agency</Link>
                }
                {
                    !(authStore.isAdmin() || authStore.isManager())
                    && <Link to="/" className="col-span-3 text-white text-2xl">Advertising agency</Link>
                }

                <div className="col-span-4"></div>
                <div className="col-span-1"></div>
                { 
                    authStore.isAuthenticated
                    ? <Link to="/profile" className="col-span-1 text-white text-right">{authStore.getUserEmail()}</Link>
                    : <Link to="/register" className="col-span-1 text-white text-right">Register</Link> 
                }

                { 
                    authStore.isAuthenticated 
                    ? <button onClick={logout} className="col-span-1 text-white text-right">Logout</button> 
                    : <Link to="/login" className="col-span-1 text-white text-right">Login</Link>
                }
            </div>
        </div>
    );
});

export default Nav;