import { useEffect, useState } from "react";
import Chart from "./Chart.js";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import format from "date-fns/format";

export default function App() {
  const [data, setdata] = useState([]);
  const [from, setFrom] = useState('');
  const [to, setTo] = useState('');
  const [amount, setAmount] = useState('')
  const [options, setOptions] = useState([]);
  const [fromDate, setFromDate] = useState('');
  const [toDate, setToDate] = useState('');

  const fetchChartData = async () => {
    const fDate = format(fromDate, 'dd-MM-yyyy')
    const tDate = format(toDate, 'dd-MM-yyyy')
    var getUrl = process.env.CURRENCY_API_ENDPOINT || "https://localhost:7035";
    const res = await fetch(getUrl + "/api/CurrencyRate/" + from + "/" + to + "/" + fDate + "/" + tDate)
      .then((response) => response.json())
      .then((data) => callBack(data))
  };

  function callBack(data) {
    console.log(data);
    var result = {
      data: data
    }
    console.log(result);
    setdata(result?.data);
  }

  function fromChange(e) {
    setFrom(e.target.value);
  }
  const fetchData = () => {
    const results = []
    // Fetch data
    var getUrl = process.env.CURRENCY_API_ENDPOINT || "https://localhost:7035";
    fetch(getUrl + "/api/CurrencyRate/GetCurrencies")
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
    fetchData();
  }, []);

  function toChange(e) {
    setTo(e.target.value);
  }

  return (
    <div className="row box">
      <div class="col-4">
        <div class="mb-3">
          <label class="form-label">From</label>
          <select class="form-select" onChange={e => fromChange(e)}>
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
          <select class="form-select" onChange={e => toChange(e)}>
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
          <label class="form-label">From Date</label>
          <DatePicker selected={fromDate} onChange={(date) => setFromDate(date)} dateFromat='YYYY-MM-dd' />

        </div>

        <div class="mb-3">
          <label class="form-label">To Date</label>
          <DatePicker selected={toDate} onChange={(date) => setToDate(date)} dateFromat='YYYY-MM-dd' />

        </div>

        <div className="col">
          <button class="btn btn-primary" onClick={fetchChartData} >
            Show Chart
          </button>
        </div>
      </div>

      <div class="col-8">
        <Chart data={data} />
      </div>
    </div>
  );
}