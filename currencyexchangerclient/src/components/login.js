import React, { useState, useEffect } from 'react';
import { useNavigate } from "react-router-dom";

function Login() {

    const [userName, setUserName] = useState('');
    const [password, setPassword] = useState('');
    const navigate = useNavigate();

    useEffect(() => {

        // Trigger the fetch
        localStorage.removeItem("token");
    }, []);

    function userNameChange(e) {
        setUserName(e.target.value);
    }

    function passordChange(e) {
        setPassword(e.target.value);
    }

    const  userLogin = () => {
        var getUrl = process.env.CURRENCY_API_ENDPOINT || "https://localhost:7035";
        var loginData = {
            UserName: userName,
            Password: password
        }
        fetch(getUrl + "/api/Auth/Login", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            },
            body: JSON.stringify(loginData)
        })
        .then((response) => response.json())
        .then((data) => {
            var tokenData = data;
            localStorage.setItem("token", tokenData);
            navigate("/exchange");
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
                        type="password" onChange={e => passordChange(e)}
                    />
                    <label for="txtPassword">Password</label>
                </div>
                <button class="btn btn-primary" onClick={userLogin} >
                    Login
                </button>
                
                <a class="btn btn-primary" style={{marginLeft: '2em'}} href="/register" >Sign Up</a>
                {/* <Router>
                    <div>
                        <NavBar />
                        <Route path="/exchange" component={exchange } />
                    </div>
                </Router> */}
            </div>
        </div>
    )
}

export default Login