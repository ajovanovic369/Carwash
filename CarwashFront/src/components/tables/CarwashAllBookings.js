import React, { useState, useEffect } from "react";
import DataTable from "react-data-table-component";
import { makeStyles } from "@material-ui/core/styles";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import Typography from "@material-ui/core/Typography";
import TextField from "@material-ui/core/TextField";
import Button from "@material-ui/core/Button";
import CssBaseline from "@material-ui/core/CssBaseline";
import Grid from "@material-ui/core/Grid";


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


const columns = [
    {
        name: "carWashId",
        selector: (row) => row.carWashId,
        width: "150px",
    },
    {
        name: "serviceId",
        selector: (row) => row.serviceId,
        width: "150px",
    },
    {
        name: "schedulingId",
        selector: (row) => row.schedulingId,
        width: "150px",
    },
    {
        name: "appointment",
        selector: (row) => row.appointment,
        width: "500px",
    },
    {
        name: "price",
        selector: (row) => row.price,
        width: "150px",
    },
];

function CarwashShops(props) {
    const classes = useStyles();
    const token = localStorage.getItem("token");
    const [error, setError] = useState(null);
    const [isLoaded, setIsLoaded] = useState(false);
    const [items, setItems] = useState([]);
    const [totalRows, setTotalRows] = useState(0);
    const [perPage, setPerPage] = useState(10);
    const [idAllEarnings, setIdAllEarnings] = useState("1");
    const [statusCheck, setStatusCheck] = useState();

    useEffect(() => {
        fetchRows(1, perPage);
        fetchData(1, perPage);
    }, [perPage, idAllEarnings]);

    const fetchRows = async (page, per_page) => {
        fetch(
            `https://localhost:7090/api/earnings/allbookingbycarwash/${idAllEarnings}?page=${page}&recordsPerPage=${per_page}`,
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
                setTotalRows(parseInt(res.headers.get('count')));
                if (res.status === 400) {
                    setStatusCheck(400);
                }
                console.log("Total Rows:" + totalRows)
            });

    };

    const fetchData = async (page, per_page) => {
        fetch(
            `https://localhost:7090/api/earnings/allbookingbycarwash/${idAllEarnings}?page=${page}&recordsPerPage=${per_page}`,
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
            .then((res) => res.json(
            ))
            .then(
                (result) => {
                    setIsLoaded(true);
                    setItems(result);
                },

                (error) => {
                    setIsLoaded(true);
                    setError(error);
                }
            );
    };


    const handlePageChange = page => {
        fetchRows(page, perPage);
        fetchData(page, perPage);
    };

    const handlePerRowsChange = async (newPerPage, page) => {
        setPerPage(newPerPage);
    };


    const backToAllCarwashes = () => {
        window.location.href = "/chooseearnings";
    };

    const handleSubmitByCarwash = (e) => {
        setIdAllEarnings(e.target.carwashid.value);
        e.preventDefault();
    }

    const handleSubmitReset = (e) => {
        setIdAllEarnings(1)
        e.preventDefault();
    }

    if (statusCheck === 400) {
        return <div>Carwash not found! Please set another carwash!
            <Grid >
                <form onSubmit={handleSubmitByCarwash}>
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
            </Grid></div>;
    }
    if (error) {
        return <div>Error: {error.message}</div>;
    } else if (!isLoaded) {
        return <div>Loading...</div>;
    } else {
        return (
            <React.Fragment>


                <div className={classes.root}>
                    <AppBar position="static">
                        <Toolbar>
                            <Typography variant="h6" className={classes.title}>
                            </Typography>
                        </Toolbar>
                    </AppBar>
                </div>

                <div className="App">
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
                            <form onSubmit={handleSubmitByCarwash}>
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


                    &emsp;&emsp;
                    <DataTable
                        columns={columns}
                        data={items}
                        pagination
                        paginationServer
                        paginationTotalRows={totalRows}
                        onChangePage={handlePageChange}
                        onChangeRowsPerPage={handlePerRowsChange}
                    />
                </div>
            </React.Fragment>

        );
    }
}

export default CarwashShops;
