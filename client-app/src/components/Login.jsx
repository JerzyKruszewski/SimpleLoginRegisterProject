import React, { Component } from "react";
import { login } from "../api/api.js";

export class Login extends Component {
    constructor(props) {
        super(props);
        this.state = {
            username: undefined,
            password: undefined,
            loggedUsername: undefined
        };

        this.handleLoginChange = this.handleLoginChange.bind(this);
        this.handlePasswordChange = this.handlePasswordChange.bind(this);
        this.loginUser = this.loginUser.bind(this);
    }

    handleLoginChange(event) {
        this.setState(state => {
            state.username = event.target.value;
        });
    }

    handlePasswordChange(event) {
        this.setState(state => {
            state.password = event.target.value;
        });
    }

    loginUser = async (event) => {
        event.preventDefault();
        const user = await this.getUser();
        console.log(user);

        this.setState(state => {
            state.loggedUsername = user.login;
            state.username = undefined;
            state.password = undefined;
        });

        console.log(this.state.loggedUsername);

        this.forceUpdate();
    }

    getUser = async () => {
        const userResponse = await login(this.state.username, this.state.password);
        const user = userResponse.data;

        return user;
    }

    render() {
        return (
            <>
                {this.state.loggedUsername !== undefined ? (
                <p>
                    Logged as: {this.state.loggedUsername}
                </p>) : (
                <p>
                    Please log in.
                </p>)}

                <form className="login-form">
                    <label>Username:</label>
                    <input id="login-username" placeholder="Username" onChange={this.handleLoginChange}
                        value={this.state.username} required />
    
                    <label>Password:</label>
                    <input id="login-password" placeholder="Password" type="password" onChange={this.handlePasswordChange}
                        value={this.state.password} required />
    
                    <button onClick={this.loginUser}>Submit</button>
                </form>
            </>
        );
    }
}