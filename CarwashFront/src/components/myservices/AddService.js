import React, { useState } from "react";
import Avatar from "@material-ui/core/Avatar";
import Button from "@material-ui/core/Button";
import CssBaseline from "@material-ui/core/CssBaseline";
import TextField from "@material-ui/core/TextField";
import Paper from "@material-ui/core/Paper";
import Grid from "@material-ui/core/Grid";
import LockOutlinedIcon from '@material-ui/icons/LibraryAdd';
import Typography from "@material-ui/core/Typography";
import { makeStyles } from "@material-ui/core/styles";
import swal from "sweetalert";
import backgroundPicture from "../../assets/addservice.jpg";

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

export default function AddServiceF() {
  const classes = useStyles();
  const [name, setName] = useState();
  const [price, setPrice] = useState();
  const [carWashEntityId, setCarWashEntityId] = useState();
  const token = localStorage.getItem("token");

  const handleGoBack = () => {
    window.location.href = "/myservices";
  };

  let statusCodeCustom = 0;

  async function addService(credentials) {
    return fetch("https://localhost:7090/api/services", {
      mode: "cors",
      method: "POST",
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token,
      },
      body: JSON.stringify(
        {
          "Name": credentials.name,
          "Price": parseFloat(credentials.price).toFixed(2),
          "CarWashEntityId": credentials.carWashEntityId.split(",").map(Number),
        }
      )
    })
      .then(res => {
        if (res.status === 201) {
          statusCodeCustom = 201;
        }
        else {
          statusCodeCustom = 400;
        }
      });
  }

  const handleSubmit = async (e) => {
    e.preventDefault();
    const response = await addService({
      name,
      price,
      carWashEntityId
    });

    if (statusCodeCustom === 201) {
      swal("Success", "Service added successfully !", "success", {
        buttons: false,
        timer: 2000,
      })
        .then((value) => {
          window.location.href = "/myservices";
        });
    } else {
      swal("Failed", "Something went wrong! \n Re-check your input values!", "error").then((value) => {
        window.location.href = "/addservice";
      });
    }
  }
  return (
    <Grid container className={classes.root}>
      <CssBaseline />
      <Grid item xs={false} md={7} className={classes.image} />
      <Grid item xs={12} md={5} component={Paper} elevation={6} square>
        <div className={classes.paper}>
          <Avatar className={classes.avatar}>
            <LockOutlinedIcon />
          </Avatar>
          <Typography component="h1" variant="h5">
            Add Service
          </Typography>
          <form className={classes.form} noValidate onSubmit={handleSubmit}>
            <TextField
              variant="outlined"
              margin="normal"
              required
              fullWidth
              placeholder="Name"
              id="name"
              name="name"
              label="name"
              onChange={(e) => setName(e.target.value)}
            />
            <TextField
              variant="outlined"
              margin="normal"
              required
              fullWidth
              placeholder="1234"
              type="number"
              id="price"
              name="price"
              label="price"
              onChange={(e) => setPrice(e.target.value)}
            />
            <TextField
              variant="outlined"
              margin="normal"
              required
              fullWidth
              placeholder="CARWASH ID, CARWASH ID, ..."
              id="carWashEntityId"
              name="carWashEntityId"
              label="carWashEntityId"
              onChange={(e) => setCarWashEntityId(e.target.value)}
            />
            <Button
              type="submit"
              fullWidth
              variant="contained"
              color="primary"
              className={classes.submit}
            >
              Add
            </Button>
          </form>
          <Button
            type="submit"
            fullWidth
            variant="contained"
            color="primary"
            onClick={handleGoBack}
          >
            Back
          </Button>
        </div>
      </Grid>
    </Grid>
  );
}
