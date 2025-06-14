
import { Route, Routes } from 'react-router-dom';
import './App.css';
import Loginpage from './pages/login';
import Home from './pages/home/home';



function App() {
    return (
        <Routes>
            <Route path="/" element={<Loginpage />} />
            <Route path="/home" element={<Home />} />
        </Routes>
    );
}

export default App;