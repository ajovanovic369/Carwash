import React from "react";
import { Link } from "react-router-dom";
import { makeStyles } from "@material-ui/core/styles";
import Grid from "@material-ui/core/Grid";
import Hidden from "@material-ui/core/Hidden";

import footerAdornment from "../../assets/Footer Adornment.svg";


const useStyles = makeStyles((theme) => ({
  footer: {
    backgroundColor: theme.palette.common.blue,
    width: "100%",
    zIndex: 1302,
    position: "bottom",
  },
  adornment: {
    width: "27em",
    verticalAlign: "bottom",
    [theme.breakpoints.down("md")]: {
      width: "21em",
    },
    [theme.breakpoints.down("xs")]: {
      width: "15em",
    },
  },
  mainContainer: {
    position: "absolute",
  },
  link: {
    color: "white",
    fontFamily: "Arial",
    fontSize: "0.75rem",
    fontWeight: "bold",
    textDecoration: "none",
  },
  gridItem: {
    margin: "3em",
  },
  icon: {
    height: "4em",
    width: "4em",
    [theme.breakpoints.down("xs")]: {
      height: "2.5em",
      width: "2.5em",
    },
  },
  socialContainer: {
    position: "absolute",
    marginTop: "-6em",
    right: "1.5em",
    [theme.breakpoints.down("xs")]: {
      right: "0.6em",
    },
  },
}));

export default function Footer(props) {
  const classes = useStyles();

  return (
    <footer className={classes.footer}>
      <Hidden mdDown>
        <Grid container justify="center" className={classes.mainContainer}>
          <Grid item className={classes.gridItem}>
            <Grid container direction="column" spacing={2}>
              <Grid
                item
                component={Link}
                onClick={() => props.setValue(0)}
                to="/"
                className={classes.link}
              >
                Home
              </Grid>
            </Grid>
          </Grid>
          <Grid item className={classes.gridItem}>
            <Grid container direction="column" spacing={2}>
              <Grid
                item
                component={Link}
                onClick={() => {
                  props.setValue(1);
                  props.setSelectedIndex(0);
                }}
                to="/carwashshops"
                className={classes.link}
              >
                Carshops
              </Grid>
              <Grid
                item
                component={Link}
                onClick={() => {
                  props.setValue(1);
                  props.setSelectedIndex(1);
                }}
                to="/carwashshops/filtering"
                className={classes.link}
              >
                Filtering
              </Grid>
            </Grid>
          </Grid>
          <Grid item className={classes.gridItem}>
            <Grid container direction="column" spacing={2}>
              <Grid
                item
                component={Link}
                onClick={() => {
                  props.setValue(2);
                  props.setSelectedIndex(0);
                }}
                to="/myaccount"
                className={classes.link}
              >
                My Account
              </Grid>
              <Grid
                item
                component={Link}
                onClick={() => {
                  props.setValue(2);
                  props.setSelectedIndex(1);
                }}
                to="/addreservation"
                className={classes.link}
              >
                Add Reservation
              </Grid>
              <Grid
                item
                component={Link}
                onClick={() => {
                  props.setValue(2);
                  props.setSelectedIndex(2);
                }}
                to="/deletereservation"
                className={classes.link}
              >
                Delete Reservation
              </Grid>
            </Grid>
          </Grid>
          <Grid item className={classes.gridItem}>
            <Grid container direction="column" spacing={2}>
              <Grid
                item
                component={Link}
                onClick={() => {
                  props.setValue(3);
                  props.setSelectedIndex(0);
                }}
                to="/mycarwashes"
                className={classes.link}
              >
                My Carwashes
              </Grid>
              <Grid
                item
                component={Link}
                onClick={() => {
                  props.setValue(3);
                  props.setSelectedIndex(1);
                }}
                to="/editreservation"
                className={classes.link}
              >
                Pending Reservation
              </Grid>
              <Grid
                item
                component={Link}
                onClick={() => {
                  props.setValue(3);
                  props.setSelectedIndex(2);
                }}
                to="/deletereservationbyowner"
                className={classes.link}
              >
                Delete Reservation (Owner)
              </Grid>
              <Grid
                item
                component={Link}
                onClick={() => {
                  props.setValue(3);
                  props.setSelectedIndex(3);
                }}
                to="/addcarwash"
                className={classes.link}
              >
                Add Carwash Shop
              </Grid>
              <Grid
                item
                component={Link}
                onClick={() => {
                  props.setValue(3);
                  props.setSelectedIndex(4);
                }}
                to="/editcarwash"
                className={classes.link}
              >
                Edit Carwash
              </Grid>
              <Grid
                item
                component={Link}
                onClick={() => {
                  props.setValue(3);
                  props.setSelectedIndex(5);
                }}
                to="/deletecarwash"
                className={classes.link}
              >
                Delete Carwash
              </Grid>
            </Grid>
          </Grid>
          <Grid item className={classes.gridItem}>
            <Grid container direction="column" spacing={2}>
              <Grid
                item
                component={Link}
                onClick={() => {
                  props.setValue(4);
                  props.setSelectedIndex(0);
                }}
                to="/myservices"
                className={classes.link}
              >
                My Services
              </Grid>
              <Grid
                item
                component={Link}
                onClick={() => {
                  props.setValue(4);
                  props.setSelectedIndex(1);
                }}
                to="/addservice"
                className={classes.link}
              >
                Add Service
              </Grid>
              <Grid
                item
                component={Link}
                onClick={() => {
                  props.setValue(4);
                  props.setSelectedIndex(2);
                }}
                to="/editservice"
                className={classes.link}
              >
                Edit Service
              </Grid>
              <Grid
                item
                component={Link}
                onClick={() => {
                  props.setValue(4);
                  props.setSelectedIndex(3);
                }}
                to="/deleteservice"
                className={classes.link}
              >
                Delete Service
              </Grid>
            </Grid>
          </Grid>
          <Grid item className={classes.gridItem}>
            <Grid container direction="column" spacing={2}>
              <Grid
                item
                component={Link}
                onClick={() => {
                  props.setValue(5);
                  props.setSelectedIndex(0);
                }}
                to="/chooseearnings"
                className={classes.link}
              >
                Earnings
              </Grid>

            </Grid>
          </Grid>
        </Grid>
      </Hidden>

      <img
        alt="black decorative slash"
        src={footerAdornment}
        className={classes.adornment}
      />


    </footer>
  );
}
