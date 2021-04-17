import React, { Component } from "react";
import { register } from "../api/api.js";

export class Register extends Component {
    constructor(props) {
        super(props);
        this.state = {
            username: undefined,
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
            state.login = event.target.value;
        });
    }

    handlePasswordChange(event) {
        this.setState(state => {
            state.password = event.target.value;
        });
    }

    handleConfirmChange(event) {
        this.setState(state => {
            state.confirmPassword = event.target.value;
        });
    }

    handlePermissionChange(event) {
        this.setState(state => {
            state.permissions === 0 ? state.permissions = 1 : state.permissions = 0;
        });
    }

    registerUser(event) {
        event.preventDefault();
        if (this.state.password === this.state.confirmPassword) {
            register(this.state.username, this.state.password, this.state.permissions);

            this.setState(state => {
                state.login = undefined;
                state.password = undefined;
                state.confirmPassword = undefined;
                state.permissions = 0;
            });
        }
    }

    render() {
        return (
            <form className="register-form">
                <label>Username:</label>
                <input id="username" placeholder="Username" onChange={this.handleLoginChange} required />

                <label>Password:</label>
                <input id="password" placeholder="Password" type="password" onChange={this.handlePasswordChange} required />

                <label>Confirm Password:</label>
                <input id="confirmPassword" placeholder="Confirm Password" type="password" onChange={this.handleConfirmChange} required />

                <label>
                    <input type="checkbox" name="permission" onClick={this.handlePermissionChange} /> Is user Admin
                </label>

                <button onClick={this.registerUser}>Submit</button>
            </form>
        );
    }
}