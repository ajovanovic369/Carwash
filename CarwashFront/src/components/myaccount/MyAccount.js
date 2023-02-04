import React, { useState, useEffect } from "react";
import DataTable from "react-data-table-component";
import { makeStyles } from "@material-ui/core/styles";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import Typography from "@material-ui/core/Typography";
import IconButton from "@material-ui/core/IconButton";
import MenuItem from "@material-ui/core/MenuItem";
import Menu from "@material-ui/core/Menu";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import { Button } from "@material-ui/core";
import LogoutIcon from "@material-ui/icons/ExitToApp";
import PersonIcon from "@material-ui/icons/Person";
import MyBookingTable from "../tables/MyReservationsTable";
import LocalCarWashIcon from '@material-ui/icons/LibraryBooks';
import DeleteCarwashIcon from '@material-ui/icons/DeleteForever';



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


export default function MyAccountF() {
  const classes = useStyles();
  const [anchorEl, setAnchorEl] = React.useState(null);
  const open = Boolean(anchorEl);
  const token = localStorage.getItem("token");
  const username = JSON.parse(localStorage.getItem("username"));
  const email = JSON.parse(localStorage.getItem("email"));
  const [error, setError] = useState(null);
  const [isLoaded, setIsLoaded] = useState(false);
  const [items, setItems] = useState([]);


  const handleMenu = (event) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  const handleAddReservation = () => {
    window.location.href = "/addreservation";
  };

  const handleDeleteReservation = () => {
    window.location.href = "/deletereservation";
  };

  const handleLogout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("username");
    localStorage.removeItem("email");
    window.location.href = "/";
  };

  return (
    <div className={classes.root}>
      <AppBar position="static">
        <Toolbar>
          <Typography variant="h5">Welcome back {username} !</Typography>
          <Typography variant="h6" className={classes.title}>
          </Typography>
          <IconButton onClick={handleAddReservation} color="inherit">
              <LocalCarWashIcon /> Add Reservation
            </IconButton>
            <IconButton onClick={handleDeleteReservation} color="inherit">
              <DeleteCarwashIcon /> Delete Reservation
            </IconButton>
          <div>
            <IconButton onClick={handleMenu} color="inherit">
              <PersonIcon /> Logout
            </IconButton>
            <Menu
              id="menu-appbar"
              anchorEl={anchorEl}
              open={open}
              onClose={handleClose}
            >
              <MenuItem onClick={handleLogout}>
                <Button
                  startIcon={<LogoutIcon />}
                  color="secondary"
                  variant="contained"
                >
                  Confirm Logout
                </Button>
              </MenuItem>
            </Menu>
          </div>
        </Toolbar>
      </AppBar>
      <Card className={classes.root} variant="outlined">
        <CardContent>
          <div className="App">
          <Typography variant="h3">My Reservations</Typography>
          <br></br>
          <MyBookingTable />
          </div>
        </CardContent>
      </Card>
    </div>
  );
}
