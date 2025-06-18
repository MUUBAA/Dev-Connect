
import { Route, Routes } from 'react-router-dom';
import './App.css';
import Loginpage from './pages/login';
import Home from './pages/home/home';
import EmailVerification from './pages/emailVerification/emailVerification';



function App() {
    return (
        <Routes>
            <Route path="/" element={<Loginpage />} />
            <Route path="/home" element={<Home />} />
            <Route path="/verify-email" element={<EmailVerification />} />
        </Routes>
    );
}

export default App;