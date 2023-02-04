import React, { useEffect, useState } from "react";
import Table from "@material-ui/core/Table";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";
import TableContainer from "@material-ui/core/TableContainer";
import TableHead from "@material-ui/core/TableHead";
import TableRow from "@material-ui/core/TableRow";
import Paper from "@material-ui/core/Paper";
import axios from "axios";
import TextField from "@material-ui/core/TextField";
import Button from "@material-ui/core/Button";
import CssBaseline from "@material-ui/core/CssBaseline";
import Grid from "@material-ui/core/Grid";
import { makeStyles } from "@material-ui/core/styles";
import backgroundPicture from "../../assets/editreservation.jpg";


const useStyles = makeStyles((theme) => ({
  root: {
    height: "100vh",
  },
  image: {
    backgroundImage: `url(${backgroundPicture})`,
    backgroundSize: "cover",
  },
  paper: {
    margin: theme.spacing(8, 4),
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
  },
  avatar: {
    margin: theme.spacing(1),
    backgroundColor: theme.palette.secondary.main,
  },
  form: {
    width: "100%",
    marginTop: theme.spacing(1),
  },
  submit: {
    margin: theme.spacing(3, 0, 2),
  },
}));

export default function DynamicTable(props) {
  const classes = useStyles();
  const [data, setData] = useState([]);
  const token = localStorage.getItem("token");
  const [filtering, setByFiltering] = useState("https://localhost:7090/api/earnings/servicesearnings");

  const backToAllCarwashes = () => {
    window.location.href = "/chooseearnings";
  };

  const handleSubmitByYear = (e) => {
    let id = (e.target.carwashid.value);
   
    setByFiltering(`https://localhost:7090/api/earnings/servicesearnings/${id}`);
    e.preventDefault();
  }

  const handleSubmitReset = (e) => {
    setByFiltering("https://localhost:7090/api/earnings/carwashearning/1");
    e.preventDefault();
  }

  useEffect(() => {
    axios
      .get(`${filtering}`,
        {
          mode: "cors",
          method: "GET",
          withCredentials: true,
          credentials: "include",
          headers: {
            'Content-type': 'application/json',
            'Authorization': 'Bearer ' + token,
          },
        }
      )
      .then((res) => {
        setData(res.data);
        console.log("Result:", data);
      })
      .catch((error) => {
        console.log(error);
      });
  }, [filtering]);

  console.log(filtering);
  
  return (
    <TableContainer component={Paper}>
      <Grid container >
        <CssBaseline />
        <Grid>
          <br></br><br></br>
          <Button
            type="submit"
            fullWidth
            variant="contained"
            color="primary"
            onClick={backToAllCarwashes}
          >
            Back
          </Button>
        </Grid>
        <Grid>
          <br></br><br></br>
          <Button
            type="submit"
            fullWidth
            variant="contained"
            color="primary"
            onClick={handleSubmitReset}
          >
            Reset
          </Button>
        </Grid>
        &emsp;
        <Grid >
          <form onSubmit={handleSubmitByYear}>
            <TextField
              variant="outlined"
              margin="normal"
              required
              fullWidth
              type="number"
              placeholder="1"
              id="carwashid"
              name="carwashid"
              label="CarwashID"
            />
            <Button
              type="submit"
              fullWidth
              variant="contained"
              color="primary"
            >
              Search
            </Button>
          </form>
        </Grid>
      </Grid>

      <Table aria-label="simple table" stickyHeader>
        <TableHead>
          <TableRow>
            <TableCell>serviceId</TableCell>
            <TableCell align="left">price</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {data.map((row) => (
            <TableRow key={row.id}>
              <TableCell component="th" scope="row">
                {row.serviceId}
              </TableCell>
              <TableCell align="left">{row.price}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}