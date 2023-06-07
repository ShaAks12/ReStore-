import { useEffect, useState } from "react";
import { Basket } from "../../app/models/basket";
import LoadingComponent from "../../app/layout/LoadingComponent";
import { Box, IconButton, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from "@mui/material";
import agent from "../../app/Api/agent";
import { Add, Delete, Remove } from "@mui/icons-material";
import { useStoreContext } from "../../app/context/StoreContext";
import { LoadingButton } from "@material-ui/lab";

export default function BasketPage(){
    const {basket, setBasket, removeItem} = useStoreContext();
    const [loading, setLoading] = useState(false);


    function handleAddItem(productId: number){
        setLoading(true);
        agent.Basket.addItem(productId)
            .then(basket => setBasket(basket))
            .catch(error => console.log(error))
            .finally(() => setLoading(false))
    }

    function handleRemoveItem(productId: number, quantity = 1){
        setLoading(true);
        agent.Basket.removeItem(productId, quantity)
            .then(() => removeItem(productId, quantity))
            .catch(error => console.log(error))
            .finally(() => setLoading(false))
        }
    
  
    // const [loading,setLoading] = useState(true); 
    // const [basket,setBasket] = useState<Basket | null>(null);

    // useEffect(() => {
    //     agent.Basket.get1()
    //         .then(basket => setBasket(basket))
    //         .catch(error => console.log(error))
    //         .finally(() => setLoading(false))
    // }, [])

    // if (loading) return <LoadingComponent message='Loading basket...' />

    if(!basket) return <Typography variant='h3'>Your basket is empty</Typography>

    return(
        <TableContainer component={Paper}>
      <Table sx={{ minWidth: 650 }} aria-label="simple table">
        <TableHead>
          <TableRow>
            <TableCell>Product</TableCell>
            <TableCell align="right">Price</TableCell>
            <TableCell align="center">Quantity</TableCell>
            <TableCell align="right">Subtotal</TableCell>
            <TableCell align="right"></TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {basket.items.map(item => (
            <TableRow
              key={item.productId}
              sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
            >
              <TableCell component="th" scope="row">
                <Box display = 'flex' alignItems='center'>
                    <img src={item.pictureUrl} alt={item.name} style={{height: 50, marginRight: 20}} />
                    <span>{item.name}</span>
                </Box>
                
              </TableCell>
              <TableCell align="right">{(item.price/100).toFixed(2)}</TableCell>
              <TableCell align="center">
                  <LoadingButton loading={loading} onClick={() => handleRemoveItem(item.productId)} color='error' >
                    <Remove />
                  </LoadingButton>
                  {item.quantity}
                  <LoadingButton loading={loading} onClick={() => handleAddItem(item.productId)} color='secondary'>
                    <Add />
                  </LoadingButton>


              </TableCell>
              <TableCell align="right">{(item.price / 100) * item.quantity}</TableCell>
              <TableCell align="right">
                    <LoadingButton loading={loading} onClick={() => handleRemoveItem(item.productId, item.quantity)} color='error'>
                        <Delete />
                    </LoadingButton>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
        
    )
}