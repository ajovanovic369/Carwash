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
import backgroundPicture from "../../assets/addcarwash.jpg";

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

export default function AddCarwashF() {
  const classes = useStyles();
  const [name, setName] = useState();
  const [address, setAddress] = useState();
  const [openingHours, setOpeningHours] = useState();
  const [closingHours, setClosingHours] = useState();
  const [carWashServiceId, setCarWashServiceId] = useState();
  const token = localStorage.getItem("token");

  const handleGoBack = () => {
    window.location.href = "/mycarwashes";
  };

  let statusCodeCustom = 0;

  async function addCarwash(credentials) {
    return fetch("https://localhost:7090/api/carwashes", {
      mode: "cors",
      method: "POST",
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token,
      },
      body: JSON.stringify(
        {
          "Name": credentials.name,
          "Address": credentials.address,
          "OpeningHours": parseInt(credentials.openingHours),
          "ClosingHours": parseInt(credentials.closingHours),
          "CarWashServiceId": credentials.carWashServiceId.split(",").map(Number),
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
    const response = await addCarwash({
      name,
      address,
      openingHours,
      closingHours,
      carWashServiceId
    });

    if (statusCodeCustom === 201) {
      swal("Success", "Carwash added successfully !", "success", {
        buttons: false,
        timer: 2000,
      })
        .then((value) => {
          window.location.href = "/mycarwashes";
        });
    } else {
      swal("Failed", "Something went wrong! \n Re-check your input values!", "error").then((value) => {
        window.location.href = "/addcarwash";
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
            Add Carwash
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
              placeholder="ADDRESS 21"
              id="address"
              name="address"
              label="address"
              onChange={(e) => setAddress(e.target.value)}
            />
            <TextField
              variant="outlined"
              margin="normal"
              required
              fullWidth
              placeholder="9"
              type="number"
              id="openingHours"
              name="openingHours"
              label="openingHours"
              onChange={(e) => setOpeningHours(e.target.value)}
            />
            <TextField
              variant="outlined"
              margin="normal"
              required
              fullWidth
              placeholder="17"
              type="number"
              id="closingHours"
              name="closingHours"
              label="closingHours"
              onChange={(e) => setClosingHours(e.target.value)}
            />
            <TextField
              variant="outlined"
              margin="normal"
              required
              fullWidth
              placeholder="SERVICE ID, SERVICE ID ..."
              id="carWashServiceId"
              name="carWashServiceId"
              label="carWashServiceId"
              onChange={(e) => setCarWashServiceId(e.target.value)}
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
