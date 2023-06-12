import {
  LineChart,
   // Bar,
    XAxis,
    YAxis,
    CartesianGrid,
    Tooltip,
    Legend,
    Line,
    ResponsiveContainer,
  } from "recharts";
  
  export default function Chart({ data }) {
    return (
    <ResponsiveContainer width="100%" height={400}>
   <LineChart width={500} height={300} data={data}>
    <XAxis dataKey="date" tickFormatter="yyyy/MM/dd"/>
    <YAxis dataKey="exchageRate"/>
    <CartesianGrid stroke="#eee" strokeDasharray="5 5"/>
    <Line type="monotone" dataKey="date" stroke="#8884d8" />
    <Line type="monotone" dataKey="exchageRate" stroke="#82ca9d" />
        </LineChart>
      </ResponsiveContainer>
    );
  }