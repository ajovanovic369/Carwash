import React, { useEffect, useState } from "react";
import Table from "@material-ui/core/Table";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";
import TableContainer from "@material-ui/core/TableContainer";
import TableHead from "@material-ui/core/TableHead";
import TableRow from "@material-ui/core/TableRow";
import Paper from "@material-ui/core/Paper";
import axios from "axios";

export default function DynamicTable() {
  const [data, setData] = useState([]);
  const token = localStorage.getItem("token");
  const [error, setError] = useState();
  const [statusCheck, setStatusCheck] = useState();

  useEffect(() => {
    axios
      .get("https://localhost:7090/api/booking/myreservations",
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
        setData(res.data);
        if (res.status === 404) {
          setStatusCheck(404);
        }
        console.log("Result:", data);
      })
      .catch((error) => {
        console.log(error);
        setError(error);
      });
  }, []);

  if (statusCheck === 404) {
    return <div>There is no reservations for your account yet.</div>;
  } else {
    return (
      <TableContainer component={Paper}>
        <Table aria-label="simple table" stickyHeader>
          <TableHead>
            <TableRow>
              <TableCell>Appointment</TableCell>
              <TableCell align="right">Id</TableCell>
              <TableCell align="right">Status</TableCell>
              <TableCell align="right">Price</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {data.map((row) => (
              <TableRow key={row.id}>
                <TableCell component="th" scope="row">
                  {row.appointment}
                </TableCell>
                <TableCell align="right">{row.id}</TableCell>
                <TableCell align="right">{row.status}</TableCell>
                <TableCell align="right">{row.price}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    );
  }
}