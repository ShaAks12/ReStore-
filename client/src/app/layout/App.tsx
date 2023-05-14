import { Container, createTheme, CssBaseline, ThemeProvider} from "@mui/material";
import { useState } from "react";
import { Outlet } from "react-router-dom";
import Catalog from "../../features/catalog/Catalog";
import Header from "./Header";
import { ToastContainer } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';


function App() {
  const [darkMode,setDarkMode] = useState(false);
  const paletteType = darkMode ? 'dark' : 'light' //if it true the set dark mode else light mode
  const theme = createTheme({
    palette:{
      mode: paletteType,
      background:{
        default: paletteType === 'light'? '#eaeaea': '#121212'
      }
      
    }
  })  

  function handleThemeChange() {
    setDarkMode(!darkMode);
  }
    return (
    <ThemeProvider theme={theme}>
      <ToastContainer position="bottom-right" hideProgressBar theme="colored" />
      <CssBaseline />
      <Header darkMode={darkMode} handleThemeChange={handleThemeChange}/>
      <Container>
          <Outlet />
      </Container>
    </ThemeProvider>
    
  );
}

export default App;
