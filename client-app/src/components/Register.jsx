import React, { Component } from "react";
import { login, register } from "../api/api.js";

export class Register extends Component {
    constructor(props) {
        super(props);
        this.state = {
            login: undefined,
            password: undefined,
            confirmPassword: undefined,
            permissions: 0
        };

        this.handleLoginChange = this.handleLoginChange.bind(this);
        this.handlePasswordChange = this.handlePasswordChange.bind(this);
        this.handleConfirmChange = this.handleConfirmChange.bind(this);
        this.handlePermissionChange = this.handlePermissionChange.bind(this);
        this.registerUser = this.registerUser.bind(this);
    }

    handleLoginChange(event) {
        this.setState(state => {
            state.login = event.target.value
        });
    }

    handlePasswordChange(event) {
        this.setState(state => {
            state.password = event.target.value
        });
    }

    handleConfirmChange(event) {
        this.setState(state => {
            state.confirmPassword = event.target.value
        });
    }

    handlePermissionChange(event) {
        this.setState(state => {
            event.target.value === true ? state.permissions = 1 : state.permissions = 0;
        });
    }

    registerUser() {
        if (this.state.password === this.state.confirmPassword) {
            register(this.state.login, this.state.password, this.state.permissions);
        }
    }

    render() {
        return (
            <div className="register-form">
                <label>Login:</label>
                <input id="username" placeholder="Login" onChange={this.handleLoginChange} required />

                <label>Password:</label>
                <input id="password" placeholder="Password" type="password" onChange={this.handlePasswordChange} required />

                <label>Confirm Password:</label>
                <input id="confirmPassword" placeholder="Confirm Password" type="password" onChange={this.handleConfirmChange} required />

                <button onClick={this.registerUser}>Submit</button>

                <label>
                    <input type="checkbox" name="permission" onClick={this.handlePermissionChange} /> Is user Admin
                </label>
            </div>
        );
    }
}