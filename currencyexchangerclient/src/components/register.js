import React, { useState, useEffect } from 'react';
import { useNavigate } from "react-router-dom";

function Register() {

    const [userName, setUserName] = useState('');
    const [password, setPassword] = useState('');
    const [role, setRole] = useState('');
    const navigate = useNavigate();

    function userNameChange(e) {
        setUserName(e.target.value);
    }

    function passwordChange(e) {
        setPassword(e.target.value);
    }

    function roleChange(e) {
        var index = e.nativeEvent.target.selectedIndex;
        setRole(e.nativeEvent.target[index].text);
    }

    const userRegistration = () => {
        var getUrl = process.env.CURRENCY_API_ENDPOINT || "https://localhost:7035";
        var registerData = {
            UserName: userName,
            Password: password,
            Roles: [role]
        }
        fetch(getUrl + "/api/Auth/Register", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            },
            body: JSON.stringify(registerData)
        })
            .then((response) => response.json())
            .then((data) => {
                alert(data);
                navigate("/");
            });
    }

    return (
        <div class="row centre-box">
            <div class="col-8">
                <div class="form-floating mb-3">
                    <input id="txtEmail" className="form-control" placeholder="Email"
                        type="text" onChange={e => userNameChange(e)}
                    />
                    <label for="txtEmail">Email</label>
                </div>
                <div class="form-floating mb-3">
                    <input id="txtPassword" className="form-control" placeholder="Password"
                        type="password" onChange={e => passwordChange(e)}
                    />
                    <label for="txtPassword">Password</label>
                </div>
                <div class="mb-3">
                    <label for="txtRole">Role</label>
                    <select className="form-select" onChange={e => roleChange(e)}>
                        <option key="Select">--Select--</option>
                        <option key="Reader">Reader</option>
                        <option key="Writer">Writer</option>
                        <option key="Admin">Admin</option>

                    </select>
                </div>
                <button class="btn btn-primary" onClick={userRegistration} >
                    Register
                </button>
            </div>
        </div>
    )
}

export default Register