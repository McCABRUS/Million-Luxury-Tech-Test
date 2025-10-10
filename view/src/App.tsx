import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import PropertyListPage from './pages/PropertyListPage';
import PropertyDetailPage from './pages/PropertyDetailPage';

export default function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Navigate to="/properties" replace />} />
        <Route path="/properties" element={<PropertyListPage/>} />
        <Route path="/properties/:id" element={<PropertyDetailPage/>} />
      </Routes>
    </BrowserRouter>
  );
}