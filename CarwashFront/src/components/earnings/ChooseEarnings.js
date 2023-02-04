import React from "react";
import Button from "@material-ui/core/Button";
import CssBaseline from "@material-ui/core/CssBaseline";
import Paper from "@material-ui/core/Paper";
import Grid from "@material-ui/core/Grid";
import Typography from "@material-ui/core/Typography";
import { makeStyles } from "@material-ui/core/styles";
import backgroundPicture from "../../assets/earnings.jpg";
import IconButton from "@material-ui/core/IconButton";
import LocalAtmIcon from '@material-ui/icons/LocalAtm';
import ArrowBackIcon from '@material-ui/icons/ArrowBack';
import ShowChartIcon from '@material-ui/icons/ShowChart';


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

export default function Signin() {
    const classes = useStyles();

    const handleBackToMain = () => {
        window.location.href = "/";
    };

    const handleToEarnings = () => {
        window.location.href = "/earnings";
    };

    const handleToEarningsAggregate = () => {
        window.location.href = "/earningsaggregate";
    };

    const handleToEarningsService = () => {
        window.location.href = "/earningsservice";
    };

    const handleToAllBookings = () => {
        window.location.href = "/carwashallbooking";
    };

    return (
        <Grid container className={classes.root}   >
            <CssBaseline />
            <Grid item xs={false} md={7} className={classes.image} />
            <Grid item xs={12} md={5} component={Paper} elevation={6} square>
                <div className={classes.paper} >
                    <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        color="primary"
                        onClick={handleToAllBookings}
                        size="large"
                    >
                        <IconButton onClick={handleToAllBookings} color="inherit" >
                            <ShowChartIcon /> <Typography variant="h7">All Reservations (by Carwash)</Typography>
                        </IconButton>
                    </Button>
                    <br></br><br></br><br></br>
                    <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        color="primary"
                        onClick={handleToEarnings}
                        size="large"
                    >
                        <IconButton onClick={handleToEarnings} color="inherit" >
                            <LocalAtmIcon /> <Typography variant="h7">Carwash Earning</Typography>
                        </IconButton>
                    </Button>
                    <br></br><br></br><br></br>
                    <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        color="primary"
                        onClick={handleToEarningsAggregate}
                        size="large"
                    >
                        <IconButton onClick={handleToEarningsAggregate} color="inherit" >
                            <LocalAtmIcon /> <Typography variant="h7">Carwash Aggregate</Typography>
                        </IconButton>
                    </Button>
                    <br></br><br></br><br></br>
                    <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        color="primary"
                        onClick={handleToEarningsService}
                        size="large"
                    >
                        <IconButton onClick={handleToEarningsService} color="inherit" >
                            <LocalAtmIcon /> <Typography variant="h7">Services Earnings</Typography>
                        </IconButton>
                    </Button>
                    <br></br><br></br><br></br><br></br>
                    <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        color="primary"
                        onClick={handleBackToMain}
                        size="large"
                    >
                        <IconButton onClick={handleBackToMain} color="inherit" >
                            <ArrowBackIcon /> <Typography variant="h7">Back</Typography>
                        </IconButton>
                    </Button>
                </div>
            </Grid>
        </Grid>
    );
}
