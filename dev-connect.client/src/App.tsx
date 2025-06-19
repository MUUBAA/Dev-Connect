
import { Route, Routes } from 'react-router-dom';
import './App.css';
import Loginpage from './pages/login';
import Home from './pages/home/home';
import EmailVerification from './pages/login/emailVerification';
import ForgotPassword from './pages/login/forgotPassword';
import ForgotPasswordMail from './pages/login/forgotPasswordMail';
import ResetRedirectPage from './pages/login/resetRedirectpage';



function App() {
    return (
        <Routes>
            <Route path="/" element={<Loginpage />} />
            <Route path="/home" element={<Home />} />
            <Route path="/verify-email" element={<EmailVerification />} />
            <Route path='/reset-password' element={<ForgotPassword />} />
            <Route path='/forgotpasswordmail' element={<ForgotPasswordMail />} />
            <Route path='/resetRedirectPage' element={<ResetRedirectPage />} />
        </Routes>
    );
}

export default App;