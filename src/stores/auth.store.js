import jwtDecode from "jwt-decode";
import { makeAutoObservable } from "mobx";

const initialToken = localStorage.getItem('token');

export class AuthStore{
 
    isAuthenticated = !!initialToken;
    isLoading = false;
    
    constructor(){
        makeAutoObservable(this);
    }

    setIsAuthenticated(payload){
        this.isAuthenticated = payload;
    }

    getUserEmail(){
        return jwtDecode(localStorage.getItem('token')).email;
    }

    getUserId(){
        return jwtDecode(localStorage.getItem('token')).id;
    }

    isAdmin(){
        const token = localStorage.getItem('token');
        return (token !== null ? jwtDecode(token).role === 'admin' : false);
    }

    reset(){
        this.isAuthenticated = false;
        this.isLoading = false;
        this.isAdmin = false;
    }

}

export default new AuthStore();