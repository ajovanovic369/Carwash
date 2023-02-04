import React, { useState } from "react";
import { ThemeProvider } from "@material-ui/styles";
import { BrowserRouter, Route, Switch } from "react-router-dom";
import theme from "./ui/Theme";
import Header from "./ui/Header";
import Footer from "./ui/Footer";
import LandingPage from "./LandingPage";
import CarwashShops from "./CarwashShops";
import AddCarWash from "./mycarwashes/AddCarwash";
import EditCarwash from "./mycarwashes/EditCarwash";
import DeleteCarwash from "./mycarwashes/DeleteCarwash";
import AddService from "./myservices/AddService";
import EditService from "./myservices/EditService";
import DeleteService from "./myservices/DeleteService";
import AddReservation from "./myaccount/AddReservation";
import EditReservation from "./mycarwashes/EditReservation";
import DeleteReservation from "./myaccount/DeleteReservation";
import DeleteReservationByOwner from "./mycarwashes/DeleteReservationByOwner";
import MyAccount from "./myaccount/MyAccount";
import Register from "./Register";
import LogIn from "./LogIn";
import MyServices from "./myservices/MyServices";
import MyCarwashes from "./mycarwashes/MyCarwashes";
import ChooseEarnings from "./earnings/ChooseEarnings";
import AllCarwashesFiltering from "./tables/AllCarwashesFiltering";
import CarwashEarning from "./tables/CarwashEarning";
import CarhWashEarningsAggregate from "./tables/CarwashEarningsAggregate";
import ServiceEarnings from "./tables/ServiceEarnings";
import CarwashAllBookings from "./tables/CarwashAllBookings";



function App() {
  const [selectedIndex, setSelectedIndex] = useState(0);
  const [value, setValue] = useState(0);
  const token = localStorage.getItem("token");

  if (!token) {
    return (
      <ThemeProvider theme={theme}>
        <BrowserRouter>
          <Header
            value={value}
            setValue={setValue}
            selectedIndex={selectedIndex}
            setSelectedIndex={setSelectedIndex}
          />
          <Switch>
            <Route
              exact
              path="/"
              render={(props) => (
                <LandingPage
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/carwashshops"
              render={(props) => (
                <CarwashShops
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/addcarwash"
              render={(props) => (
                <LogIn
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/editcarwash"
              render={(props) => (
                <LogIn
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/myaccount"
              render={(props) => (
                <LogIn
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/mycarwashes"
              render={(props) => (
                <LogIn
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/deletecarwash"
              render={(props) => (
                <LogIn
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/register"
              render={(props) => (
                <Register
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/login"
              render={(props) => (
                <LogIn
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/myservices"
              render={(props) => (
                <LogIn
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/addservice"
              render={(props) => (
                <LogIn
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/editservice"
              render={(props) => (
                <LogIn
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/deleteservice"
              render={(props) => (
                <LogIn
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/addreservation"
              render={(props) => (
                <LogIn
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/editreservation"
              render={(props) => (
                <LogIn
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/deletereservation"
              render={(props) => (
                <LogIn
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/deletereservationbyowner"
              render={(props) => (
                <LogIn
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/chooseearnings"
              render={(props) => (
                <LogIn
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/earnings"
              render={(props) => (
                <LogIn
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/earningsaggregate"
              render={(props) => (
                <LogIn
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/earningsservice"
              render={(props) => (
                <LogIn
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/carwashallbooking"
              render={(props) => (
                <LogIn
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/carwashshops/filtering"
              render={(props) => (
                <LogIn
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
          </Switch>
          <Footer setValue={setValue} setSelectedIndex={setSelectedIndex} />
        </BrowserRouter>
      </ThemeProvider>
    );
  }

  if (token) {
    return (
      <ThemeProvider theme={theme}>
        <BrowserRouter>
          <Header
            value={value}
            setValue={setValue}
            selectedIndex={selectedIndex}
            setSelectedIndex={setSelectedIndex}
          />
          <Switch>
            <Route
              exact
              path="/"
              render={(props) => (
                <LandingPage
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/carwashshops"
              render={(props) => (
                <CarwashShops
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/addcarwash"
              render={(props) => (
                <AddCarWash
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/editcarwash"
              render={(props) => (
                <EditCarwash
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/deletecarwash"
              render={(props) => (
                <DeleteCarwash
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/myaccount"
              render={(props) => (
                <MyAccount
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/mycarwashes"
              render={(props) => (
                <MyCarwashes
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/register"
              render={(props) => (
                <MyAccount
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/login"
              render={(props) => (
                <MyAccount
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/myservices"
              render={(props) => (
                <MyServices
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/addservice"
              render={(props) => (
                <AddService
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/editservice"
              render={(props) => (
                <EditService
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/deleteservice"
              render={(props) => (
                <DeleteService
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/addreservation"
              render={(props) => (
                <AddReservation
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/editreservation"
              render={(props) => (
                <EditReservation
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/deletereservation"
              render={(props) => (
                <DeleteReservation
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/deletereservationbyowner"
              render={(props) => (
                <DeleteReservationByOwner
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/chooseearnings"
              render={(props) => (
                <ChooseEarnings
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/carwashshops/filtering"
              render={(props) => (
                <AllCarwashesFiltering
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/earnings"
              render={(props) => (
                <CarwashEarning
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/earningsaggregate"
              render={(props) => (
                <CarhWashEarningsAggregate
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/earningsservice"
              render={(props) => (
                <ServiceEarnings
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
            <Route
              exact
              path="/carwashallbooking"
              render={(props) => (
                <CarwashAllBookings
                  {...props}
                  setValue={setValue}
                  setSelectedIndex={setSelectedIndex}
                />
              )}
            />
          </Switch>
          <Footer setValue={setValue} setSelectedIndex={setSelectedIndex} />
        </BrowserRouter>
      </ThemeProvider>
    );
  }
}

export default App;
