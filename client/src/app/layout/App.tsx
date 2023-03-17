import { Container, createTheme, CssBaseline, ThemeProvider} from "@mui/material";
import { useState } from "react";
import Catalog from "../../features/catalog/Catalog";
import Header from "./Header";


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
      <CssBaseline />
      <Header darkMode={darkMode} handleThemeChange={handleThemeChange}/>
      <Container>
          <Catalog />
      </Container>
    </ThemeProvider>
    
  );
}

export default App;
