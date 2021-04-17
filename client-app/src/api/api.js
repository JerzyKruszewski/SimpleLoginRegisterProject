import { ToastsStore } from "react-toasts";
var axios = require("axios");

const config = {
    headers: {
      "Access-Control-Allow-Origin": "*",
      "Access-Control-Allow-Methods": "GET,PUT,POST,DELETE,PATCH,OPTIONS"
    }
  };

const loginRoute = "http://localhost:16560/api/users";

export const login = async (login, password) => {
    try {
        return await axios.get(`${loginRoute}/login/${login}/${password}`, config);
    } catch (error) {
        ToastsStore.error("Error logging: " + error, 3000, "custom-toast");
    }
};

export const register = async (login, password, permissions) => {
    try {
        return await axios.post(`${loginRoute}/register`, {login, password, permissions}, config);
    } catch (error) {
        ToastsStore.error(
            "Error registering: " + error,
            3000,
            "custom-toast"
        );
    } 
};