import React, { useEffect, useState } from "react";
import Table from "@material-ui/core/Table";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";
import TableContainer from "@material-ui/core/TableContainer";
import TableHead from "@material-ui/core/TableHead";
import TableRow from "@material-ui/core/TableRow";
import Paper from "@material-ui/core/Paper";
import avatar from "../../assets/avatar.webp";
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
  const [filtering, setByFiltering] = useState("https://localhost:7090/api/carwashes");

  const backToAllCarwashes = () => {
    window.location.href = "/carwashshops";
  };

  const handleSubmitByName = (e) => {
    setByFiltering("https://localhost:7090/api/carwashes/filters?name=" + e.target.name.value);
    e.preventDefault();
  }

  const handleSubmitByAddress = (e) => {
    setByFiltering("https://localhost:7090/api/carwashes/filters?address=" + e.target.address.value);
    e.preventDefault();
  }

  const handleSubmitReset = (e) => {
    setByFiltering("https://localhost:7090/api/carwashes");
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
          <form onSubmit={handleSubmitByName}>
            <TextField
              variant="outlined"
              margin="normal"
              required
              fullWidth
              placeholder="search..."
              id="name"
              name="name"
              label="name"
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
        &emsp;
        <Grid>
          <form onSubmit={handleSubmitByAddress}>
            <TextField
              variant="outlined"
              margin="normal"
              required
              fullWidth
              placeholder="search.."
              id="address"
              name="address"
              label="address"
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
            <TableCell>Name</TableCell>
            <TableCell align="right">Picture</TableCell>
            <TableCell align="right">CarwashId</TableCell>
            <TableCell align="right">address</TableCell>
            <TableCell align="right">openingHours</TableCell>
            <TableCell align="right">closingHours</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {data.map((row) => (
            <TableRow key={row.id}>
              <TableCell component="th" scope="row">
                {row.name}
              </TableCell>
              <TableCell align="right"><img src={avatar} width={60}></img></TableCell>
              <TableCell align="right">{row.id}</TableCell>
              <TableCell align="right">{row.address}</TableCell>
              <TableCell align="right">{row.openingHours}</TableCell>
              <TableCell align="right">{row.closingHours}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}