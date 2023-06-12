import logo from './logo.svg';
import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import NavBar from './components/NavBar';
import Header from './components/Header';
import Exchange from './components/currencyexchange';
import Chart from './components/currencyexchangechart';

function App() {
  return (
    <Router>
      <Header />
      <NavBar />
      <Routes>
        <Route path="/" element={ <Exchange />} />
        <Route path="/exchange" element={<Exchange />} />
        <Route path="/chart" element={<Chart />} />
      </Routes>
    </Router>
  )
}

export default App;
