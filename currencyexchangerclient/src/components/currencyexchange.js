import React, { useState, useEffect } from 'react';
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import format from "date-fns/format";

function Exchange() {
    const [options, setOptions] = useState([]);
    const [currenncyValue, setCurrenncyValue] = useState("");
    const [from, setFrom] = useState('');
    const [fromText, setFromText] = useState('');
    const [to, setTo] = useState('');
    const [toText, setToText] = useState('');
    const [amount, setAmount] = useState('')
    const [isVisible, setVisible] = useState(false)
    const [date, setDate] = useState('');
    function fromChange(e) {
        setFrom(e.target.value);
        var index = e.nativeEvent.target.selectedIndex;
        setFromText(e.nativeEvent.target[index].text);
    }

    function toChange(e) {
        setTo(e.target.value);
        var index = e.nativeEvent.target.selectedIndex;
        setToText(e.nativeEvent.target[index].text);
    }

    function amountChange(e) {
        setAmount(e.target.value);
        console.log(amount);
    }

    const getCurrencies = () => {

        var getUrl = process.env.CURRENCY_API_ENDPOINT || "https://localhost:7035";

        if (date !== undefined && date !== null) {
         const cDate = format(date, 'dd-MM-yyyy')
            var token = localStorage.getItem("token");
            fetch(getUrl + "/api/CurrencyRate/" + from + "/" + to + "/" + amount+"/"+cDate,
                {
                    method: "GET",
                    headers: {
                        Authorization: 'Bearer ' +token
                    }
                }
            )
            .then((response) => response.json())
            .then((data) => loadCurrency(data));
        }
        else {
            var token = localStorage.getItem("token");
            fetch(getUrl + "/api/CurrencyRate/" + from + "/" + to + "/" + amount, 
                {
                    method: "GET",
                    headers: {
                        Authorization: 'Bearer ' +token
                    }
                }
            )
            .then((response) => response.json())
            .then((data) => loadCurrency(data));
        }
    }

    function loadCurrency(data) {
        setVisible(true);
        setCurrenncyValue(data);
    }
    const fetchData = () => {
        const results = []
        // Fetch data
        var getUrl = process.env.CURRENCY_API_ENDPOINT || "https://localhost:7035";
        var token = localStorage.getItem("token");
        fetch(getUrl + "/api/CurrencyRate/GetCurrencies", 
            {
                method: "GET",
                headers: {
                    Authorization: 'Bearer ' +token
                }
            })
            .then((response) => response.json())
            .then((data) => data.forEach((value) => {
                results.push({
                    key: value.currencyCode,
                    value: value.currencyCode,
                });
                setOptions([
                    { key: '--Select--', value: '' },
                    ...results
                ])
            })
            );
    }

    useEffect(() => {

        // Trigger the fetch
        fetchData();
    }, []);

    return (
        <div class="row box">
            <div class="col-4">
                <div class="mb-3">
                    <label class="form-label">From</label>
                    <select className="form-select" onChange={e => fromChange(e)}>
                        {options.map((option) => {
                            return (
                                <option value={option.value}>
                                    {option.key}
                                </option>
                            );
                        })}
                    </select>
                </div>
                <div class="mb-3">
                    <label class="form-label">To</label>
                    <select className="form-select" onChange={e => toChange(e)}>
                        {options.map((option) => {
                            return (
                                <option value={option.value}>
                                    {option.key}
                                </option>
                            );
                        })}
                    </select>
                </div>
                <div class="form-floating mb-3">
                    <input id="txtAmount" className="form-control" placeholder="Amount"
                        type="text"
                        onChange={amountChange}
                    />
                    <label for="txtAmount">Amount</label>
                </div>
                <div class="mb-3">
                    <label class="form-label">Date</label>
                    <DatePicker selected={date} onChange={(date) => setDate(date)} dateFromat='YYYY-MM-dd' />

                </div>
                <button class="btn btn-primary" onClick={getCurrencies}>
                    Convert
                </button>
            </div>
            <div class="col currency">
                {isVisible ? <label>{amount} {fromText} =  {currenncyValue} {toText}</label> : null}

            </div>

        </div>

    )
}
export default Exchange