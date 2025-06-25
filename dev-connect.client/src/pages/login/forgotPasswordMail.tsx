import { Box, Button, TextField } from "@mui/material";
import {  useNavigate } from "react-router";
import { toast, ToastContainer } from "react-toastify";
import { ForgotPassword } from "../../redux/thunk/jwtVerify";
import { useDispatch } from "react-redux";
import type  { AppDispatch } from "../../redux/stores";
import { useState } from "react";







const ForgotPasswordMail : React.FC = () => {

     const [email, setEmail] = useState("")
     const dispatch = useDispatch<AppDispatch>();
     const navigate = useNavigate();
     const [isLoading, setIsLoading] = useState(false)
    const onForgotPasswordClick = async (e: any) => {
        e.preventDefault();
        setIsLoading(true);
     try {
        await dispatch(ForgotPassword({ email })).unwrap();
        toast.success("Password reset link sent to your mail");
        navigate("/resetRedirectPage");
     } catch (error: any) {
        toast.error(typeof error === "string" ? error : "Failed to send password reset email.");
        console.error(error);
     } finally {
        setIsLoading(false)
     }
  }

    return(
        <>
        <Box sx={{
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
        }}>
            <ToastContainer />
            <Box
                sx={{
                    position: "absolute",
                    top: "50%",
                    left: "50%",
                    transform: "translate(-50%, -50%)",
                    width: { xs: "90%", sm: "60%", md: "35%" },
                    minWidth: 320,
                    maxWidth: 420,
                    bgcolor: "background.paper",
                    borderRadius: 5,
                    p: { xs: 3, md: 5 },
                    boxShadow: "0 8px 40px 0 rgba(33,203,243,0.18)",
                    display: "flex",
                    flexDirection: "column",
                    alignItems: "center",
                    justifyContent: "center",
                    gap: 3,
                    background: "rgba(255,255,255,0.95)",
                    backdropFilter: "blur(2px)",
                }}
            >
                <Box
                    sx={{
                        mb: 2,
                        display: "flex",
                        flexDirection: "column",
                        alignItems: "center",
                        width: "100%",
                    }}
                >
                    <img
                        src="https://cdn-icons-png.flaticon.com/512/561/561127.png"
                        alt="Forgot Password"
                        style={{ width: 56, marginBottom: 8, opacity: 0.85 }}
                    />
                    <Box
                        component="h2"
                        sx={{
                            fontWeight: 700,
                            fontSize: "1.6rem",
                            color: "#1976d2",
                            letterSpacing: 1,
                            mb: 0.5,
                            textAlign: "center",
                        }}
                    >
                        Forgot your password?
                    </Box>
                    <Box
                        sx={{
                            color: "#555",
                            fontSize: "1rem",
                            textAlign: "center",
                            mb: 1,
                        }}
                    >
                        Enter your email address and we'll send you a link to reset your password.
                    </Box>
                </Box>
                <TextField
                    label="Email Address"
                    variant="outlined"
                    fullWidth
                    margin="normal"
                    onChange={(e) => setEmail(e.target.value)}
                    value={email}
                    sx={{
                        background: "#f7fbfc",
                        borderRadius: 2,
                        mb: 2,
                        "& .MuiOutlinedInput-root": {
                            borderRadius: 2,
                        },
                        input: { color: "#222" },
                        "& label.Mui-focused": {
                            color: "#1976d2",
                        },
                    }}
                    InputProps={{
                        style: { fontSize: "1.05rem" },
                    }}
                    InputLabelProps={{
                        style: { fontSize: "1.05rem" },
                    }}
                    // disabled={!!email}
                />
                <Button
                    fullWidth
                    sx={{
                        background: "linear-gradient(90deg, #1976d2 0%, #21cbf3 100%)",
                        color: "#fff",
                        fontWeight: 700,
                        py: 1.5,
                        borderRadius: 2,
                        fontSize: "1.1rem",
                        boxShadow: "0 4px 20px 0 rgba(33, 203, 243, 0.18)",
                        mb: 1,
                        textTransform: "none",
                        letterSpacing: 1,
                        transition: "transform 0.2s, box-shadow 0.2s",
                        "&:hover": {
                            transform: "scale(1.04)",
                            boxShadow: "0 8px 32px 0 rgba(33, 203, 243, 0.25)",
                            background: "linear-gradient(90deg, #1565c0 0%, #00bcd4 100%)",
                        },
                    }}
                    onClick={onForgotPasswordClick}
                    disabled={isLoading}
                >
                    {isLoading ? 'Loading...' : 'Send Reset Link'}
                </Button>
            </Box>

        </Box>
        
        </>
    )
}


export default ForgotPasswordMail;