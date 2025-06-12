
import { Route, Routes } from 'react-router-dom';
import './App.css';
import Loginpage from './pages/login';



function App() {
    return (
        <Routes>
            <Route path="/" element={<Loginpage />} />
        </Routes>
    );
}

export default App;