export default class Authentication {

    static setToken(token){
        return localStorage.setItem('brain_token', token);
    }

    static isAuthenticated(){
        return !!localStorage.getItem('brain_token');
    }

    static getToken(){
        return localStorage.getItem('brain_token');
    }
    
}