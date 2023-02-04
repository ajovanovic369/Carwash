import React, { useState, useEffect } from "react";
import { makeStyles } from "@material-ui/core/styles";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import Typography from "@material-ui/core/Typography";
import IconButton from "@material-ui/core/IconButton";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import LocalCarWashIcon from "@material-ui/icons/LocalCarWash";
import EditReservationIcon from '@material-ui/icons/EditAttributes';
import EditCarwashIcon from '@material-ui/icons/Edit';
import DeleteCarwashIcon from '@material-ui/icons/DeleteForever';
import DeleteReservationOwnerIcon from '@material-ui/icons/DeleteForever';
import MyCarwashesTable from "../tables/MyCarwashesTable"


const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
  },
  rootModal: {
    "& .MuiTextField-root": {
      margin: theme.spacing(1),
      width: "25ch",
    },
  },
  menuButton: {
    marginRight: theme.spacing(2),
  },
  title: {
    flexGrow: 1,
  },
  large: {
    width: theme.spacing(20),
    height: theme.spacing(20),
  },
  paper: {
    position: "absolute",
    width: 400,
    backgroundColor: theme.palette.background.paper,
    border: "2px solid #000",
    boxShadow: theme.shadows[5],
    padding: theme.spacing(2, 4, 3),
  },
}));


export default function MyCarwashesF() {
  const classes = useStyles();
  const token = localStorage.getItem("token");
  const username = JSON.parse(localStorage.getItem("username"));
  const email = JSON.parse(localStorage.getItem("email"));


  const handleAddCarwash = () => {
    window.location.href = "/addcarwash";
  };

  const handleEditCarwash = () => {
    window.location.href = "/editcarwash";
  };

  const handleEditReservation = () => {
    window.location.href = "/editreservation";
  };

  const handleDeleteReservationByOwner = () => {
    window.location.href = "/deletereservationbyowner";
  };

  const handleDeleteCarwash = () => {
    window.location.href = "/deletecarwash";
  };

  return (
    <div className={classes.root}>
      <AppBar position="static">
        <Toolbar>
          <Typography variant="h5">Welcome back {username} !</Typography>
          <Typography variant="h6" className={classes.title}>
            Profile
          </Typography>
          <IconButton onClick={handleEditReservation} color="inherit">
            <EditReservationIcon /> <Typography variant="h5">Pending Reservations</Typography>
           </IconButton>
          <IconButton onClick={handleDeleteReservationByOwner} color="inherit">
            <DeleteReservationOwnerIcon /> <Typography variant="h5">Delete Reservation (Owner)</Typography>
          </IconButton>
          <IconButton onClick={handleAddCarwash} color="inherit">
            <LocalCarWashIcon /> <Typography variant="h5">Add Carwash</Typography>
          </IconButton>
          <IconButton onClick={handleEditCarwash} color="inherit">
            <EditCarwashIcon /> <Typography variant="h5">Edit Carwash</Typography>
          </IconButton>
          <IconButton onClick={handleDeleteCarwash} color="inherit">
            <DeleteCarwashIcon /> <Typography variant="h5">Delete Carwash</Typography>
          </IconButton>
        </Toolbar>
      </AppBar>
      <Card className={classes.root} variant="outlined">
        <CardContent>
          <div className="App">
            <Typography variant="h3">My Carwash Shops</Typography>
            <br></br>
            <MyCarwashesTable />
          </div>
        </CardContent>
      </Card>
    </div>
  );
}
