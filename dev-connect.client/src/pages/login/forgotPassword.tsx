import { Box, Button, TextField, Typography } from "@mui/material";
import { useDispatch } from "react-redux";
import type { AppDispatch } from "../../redux/stores";
import { useLocation } from "react-router";
import { resetPassword } from "../../redux/thunk/jwtVerify";
import { useState } from "react";
import { toast, ToastContainer } from "react-toastify";

const ForgotPassword: React.FC = () => {
  const dispatch = useDispatch<AppDispatch>();
  const location = useLocation();
  const query = new URLSearchParams(location.search)
  const token = query.get('token');
  const [newPassword, setNewPassword] = useState<string>("");
  const [ConfirmPassword, SetConfirmPassword] = useState<string>("");

  const resetPasswordClick = async () => {
    console.log(token);
    
    if (newPassword !== ConfirmPassword) {
      toast.info("new Password and Confirm Password doesn't match");
      return
    }
    try {
      if (token) {
        await dispatch(resetPassword({ token, newPassword })).unwrap();
        toast.success("Reset the password sucessfully");
      }
    } catch (error) {
      console.log("Failed to Reset the Password", error);
      toast.error("Failed to Reset the Password");
    }
  };

  return (
    <>
      <Box
        sx={{
          width: "100vw",
          height: "100vh",
          fontFamily: "Comfortaa, Comfortaa",
          color: "#ffffff",
          background: `linear-gradient(-45deg, #23d5abcc, #81BFDA, #81BFDA, #23d5abcc)`,
          backgroundBlendMode: "overlay",
          backgroundSize: "cover",
          backgroundPosition: "center",
          backgroundRepeat: "no-repeat",
          animation: "gradientBG 15s ease infinite",
          "@keyframes gradientBG": {
            "0%": {
              backgroundPosition: "0% 50%",
            },
            "50%": {
              backgroundPosition: "100% 50%",
            },
            "100%": {
              backgroundPosition: "0% 50%",
            },
          },
        }}
      >
        <ToastContainer />
        <Box
          sx={{
            position: "absolute",
            top: "50%",
            left: "50%",
            transform: "translate(-50%, -50%)",
            width: "30%",
            height: "55%",
            bgcolor: "#81BFDA",
            borderRadius: 4,
            p: 4,
            boxShadow: 24,
            backgroundColor: "#B1F0F7",
            display: "flex",
            flexDirection: "column",
            alignItems: "center",
            justifyContent: "center",
          }}
        >
          <Typography
            variant="h4"
            fontWeight={700}
            color="#1976d2"
            mb={3}
            sx={{
              letterSpacing: 1,
              textShadow: "0 2px 8px #21cbf355",
              fontFamily: "Comfortaa, Comfortaa",
              textAlign: "center",
            }}
          >
            Reset Password
          </Typography>
          <TextField
            label="New Password"
            variant="outlined"
            fullWidth
            value={newPassword}
            onChange={(e) => setNewPassword(e.target.value)}
            margin="normal"
            sx={{
              background: "#fff",
              borderRadius: 3,
              borderColor: "#23a6d5cc",
              mb: 2,
              color: "blue",
              "& .MuiOutlinedInput-root": {
                borderRadius: 3,
              },
              input: { color: "#222" },
              "& label.Mui-focused": {
                color: "blue", // label color when focused
              },
            }}
          />
          <TextField
            label="Confirm Password"
            variant="outlined"
            fullWidth
            value={ConfirmPassword}
            onChange={(e) => SetConfirmPassword(e.target.value)}
            margin="normal"
            sx={{
              background: "#fff",
              borderRadius: 3,
              borderColor: "#23a6d5cc",
              mb: 2,
              color: "blue",
              "& .MuiOutlinedInput-root": {
                borderRadius: 3,
              },
              input: { color: "#222" },
              "& label.Mui-focused": {
                color: "blue", // label color when focused
              },
            }}
          />

          <Button
            fullWidth
            sx={{
              bgcolor: "linear-gradient(90deg, #1976d2 0%, #21cbf3 100%)",
              color: "#fff",
              fontWeight: 600,
              py: 1.5,
              borderRadius: 2,
              fontSize: "1.1rem",
              boxShadow: "0 4px 20px 0 rgba(33, 203, 243, 0.2)",
              mb: 2,
              transition: "transform 0.2s, box-shadow 0.2s",
              "&:hover": {
                transform: "scale(1.03)",
                boxShadow: "0 8px 32px 0 rgba(33, 203, 243, 0.25)",
                bgcolor: "#1976d2",
              },
            }}
            onClick={() => resetPasswordClick()}
          >
            Submit
          </Button>
        </Box>
      </Box>
    </>
  );
};

export default ForgotPassword;
