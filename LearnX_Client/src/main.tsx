import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import App from './App.tsx'
import { Provider } from "react-redux";
import store from './redux/Store.tsx';
import { ToastContainer } from 'react-toastify';

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <ToastContainer/>
    <Provider store={store}>
    
      <App />
      
      </Provider>

      
  </StrictMode>,
)
