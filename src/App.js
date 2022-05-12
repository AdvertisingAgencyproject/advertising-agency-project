import Nav from "./components/Nav";
import Register from "./components/Register";
import Login from "./components/Login";
import Home from "./components/Home";
import ProductsAdmin from "./components/ProductsAdmin";
import FavorsAdmin from "./components/FavorsAdmin";
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import {
    BrowserRouter as Router,
    Routes,
    Route
} from "react-router-dom";
import { observer } from "mobx-react-lite";
import authStore from "./stores/auth.store";
import ProductList from "./components/ProductList";
import UserProfile from "./components/UserProfile";
import AdminPage from "./components/AdminPage";
import FavorList from "./components/FavorList";

const App = observer(() => {
    return(
        <div className="app">
            <Router>
                <Nav/>
                <Routes>
                    { !authStore.isAuthenticated && <Route exact path="/register" element={<Register/>}/> }
                    { !authStore.isAuthenticated && <Route exact path="/login" element={<Login/>}/> }
                    <Route exact path="/products" element={<ProductList/>}/>
                    <Route exact path="/favors" element={<FavorList/>}/>
                    <Route exact path="/" element={<Home/>}/>
                    { authStore.isAdmin() && <Route exact path="/admin" element={<AdminPage/>}/> }
                    { authStore.isAdmin() && <Route exact path="/admin/favors" element={<FavorsAdmin/>}/> }
                    { authStore.isAdmin() && <Route exact path="/admin/products" element={<ProductsAdmin/>}/> }
                    { authStore.isAuthenticated && <Route exact path="/profile" element={<UserProfile/>}/> }
                </Routes>
            </Router>
            <ToastContainer position="top-left" />
        </div>
    );
});

export default App;