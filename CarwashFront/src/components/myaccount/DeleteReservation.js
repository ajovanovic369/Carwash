import React, { useState } from "react";
import Avatar from "@material-ui/core/Avatar";
import Button from "@material-ui/core/Button";
import CssBaseline from "@material-ui/core/CssBaseline";
import TextField from "@material-ui/core/TextField";
import Paper from "@material-ui/core/Paper";
import Grid from "@material-ui/core/Grid";
import LockOutlinedIcon from '@material-ui/icons/DeleteForever';
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

export default function DeleteReservationF() {
    const classes = useStyles();
    const [bookingId, setBookingId] = useState();
    const token = localStorage.getItem("token");

    const handleGoBack = () => {
        window.location.href = "/myaccount";
    };

    let statusCodeCustom = 0;

    async function deleteReservation(credentials) {
        return fetch(`https://localhost:7090/api/booking/cancellation/${parseInt(bookingId)}`, {
            mode: "cors",
            method: "DELETE",
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + token,
            },

        })
            .then(res => {
                if (res.status === 200) {
                    statusCodeCustom = 200;
                }
                else {
                    statusCodeCustom = 400;
                }
            });
    }

    const handleSubmit = async (e) => {
        e.preventDefault();
        const response = await deleteReservation({

        });

        if (statusCodeCustom === 200) {
            swal("Success", "Carwash deleted successfully !", "success", {
                buttons: false,
                timer: 2000,
            })
                .then((value) => {
                    window.location.href = "/myaccount";
                });
        } else {
            swal("Failed", "Something went wrong! \n Re-check your input values!", "error").then((value) => {
                window.location.href = "/deletereservation";
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
                        Delete Reservation
                    </Typography>
                    <form className={classes.form} noValidate onSubmit={handleSubmit}>
                        <TextField
                            variant="outlined"
                            margin="normal"
                            required
                            fullWidth
                            type="number"
                            placeholder="1"
                            id="bookingId"
                            name="bookingId"
                            label="bookingId"
                            onChange={(e) => setBookingId(e.target.value)}
                        />
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            color="primary"
                            className={classes.submit}
                        >
                            Delete
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
