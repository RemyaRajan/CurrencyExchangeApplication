import logo from './logo.svg';
import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import NavBar from './components/NavBar';
import Header from './components/Header';
import Exchange from './components/currencyexchange';
import Chart from './components/currencyexchangechart';
import Login from './components/login';
import Register from './components/register';

function App() {
  return (
    <Router>
      <Header />
      <NavBar />
      <Routes>
        <Route path="/" element={ <Login />} />
        <Route path="/exchange" element={<Exchange />} />
        <Route path="/chart" element={<Chart />} />
        <Route path="/register" element={<Register />} />
      </Routes>
    </Router>
  )
}

export default App;
