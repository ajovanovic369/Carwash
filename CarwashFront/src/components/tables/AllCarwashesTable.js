import React, { useState, useEffect } from "react";
import DataTable from "react-data-table-component";
import avatar from "../../assets/avatar.webp";
import { makeStyles } from "@material-ui/core/styles";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import Typography from "@material-ui/core/Typography";
import IconButton from "@material-ui/core/IconButton";
import FilterListIcon from '@material-ui/icons/FilterList';

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
    name: "id",
    selector: (row) => row.id,
    width: "100px",
  },
  {
    name: "picture",
    cell: (row) => <img src={avatar} width={50}></img>,
    selector: (row) => row.picture,
    width: "100px",
  },
  {
    name: "name",
    selector: (row) => row.name,
    width: "200px",
  },
  {
    name: "address",
    selector: (row) => row.address,
    width: "300px",
  },
  {
    name: "openingHours",
    selector: (row) => row.openingHours,
    width: "100px",
  },
  {
    name: "closingHours",
    selector: (row) => row.closingHours,
    width: "100px",
  },
  {
    name: "services",
    selector: (row) => row.services.map(x => x.id + " " + x.name + " " + x.price + "\r\n"),
    width: "500px",
  },
];

function CarwashShops(props) {
  const classes = useStyles();
  const [error, setError] = useState(null);
  const [isLoaded, setIsLoaded] = useState(false);
  const [items, setItems] = useState([]);
  const [totalRows, setTotalRows] = useState(0);
  const [perPage, setPerPage] = useState(10);

  useEffect(() => {
    fetchRows(1, perPage);
    fetchData(1, perPage);
  }, [perPage]);

  const fetchRows = async (page, per_page) => {
    fetch(
      `https://localhost:7090/api/carwashes?page=${page}&recordsPerPage=${per_page}`
    )
      .then((res) => {
        setTotalRows(parseInt(res.headers.get('count')));
      });

  };

  const fetchData = async (page, per_page) => {
    fetch(
      `https://localhost:7090/api/carwashes?page=${page}&recordsPerPage=${per_page}`
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

  const handleFiltering = () => {
    window.location.href = "/carwashshops/filtering";
  };

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
              <IconButton onClick={handleFiltering} color="inherit">
                <FilterListIcon /> <Typography variant="h5">Filtering</Typography>
              </IconButton>
            </Toolbar>
          </AppBar>
        </div>
        <div className="App">
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
