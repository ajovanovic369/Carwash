import React, { useState, useEffect } from "react";
import { makeStyles } from "@material-ui/core/styles";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import Typography from "@material-ui/core/Typography";
import IconButton from "@material-ui/core/IconButton";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import LocalCarWashIcon from '@material-ui/icons/Build';
import EditCarwashIcon from '@material-ui/icons/Edit';
import DeleteCarwashIcon from '@material-ui/icons/DeleteForever';
import MyServicesTable from "../tables/MyServicesTable"


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


export default function MyServicesF() {
  const classes = useStyles();
  const token = localStorage.getItem("token");
  const username = JSON.parse(localStorage.getItem("username"));
  const email = JSON.parse(localStorage.getItem("email"));


  const handleAddService = () => {
    window.location.href = "/addservice";
  };

  const handleEditService = () => {
    window.location.href = "/editservice";
  };

  const handleDeleteService = () => {
    window.location.href = "/deleteservice";
  };

  return (
    <div className={classes.root}>
      <AppBar position="static">
        <Toolbar>
          <Typography variant="h5">Welcome back {username} !</Typography>
          <Typography variant="h6" className={classes.title}>
            Profile
          </Typography>
          <div>
          <IconButton onClick={handleAddService} color="inherit">
              <LocalCarWashIcon /> Add Service
            </IconButton>
            <IconButton onClick={handleEditService} color="inherit">
              <EditCarwashIcon /> Edit Service
            </IconButton>
            <IconButton onClick={handleDeleteService} color="inherit">
              <DeleteCarwashIcon /> Delete Service
            </IconButton>
          </div>
        </Toolbar>
      </AppBar>
      <Card className={classes.root} variant="outlined">
        <CardContent>
          <div className="App">
            <Typography variant="h3">My Services</Typography>
            <br></br>
            <MyServicesTable />
          </div>
        </CardContent>
      </Card>
    </div>
  );
}
