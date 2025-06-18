import { Box } from "@mui/material";
import { useDispatch } from "react-redux";
import { useLocation, useNavigate } from "react-router";
import { toast, ToastContainer } from "react-toastify";
import type { AppDispatch } from "../../redux/stores";
import { EmailVerification } from "../../redux/thunk/jwtVerify";
import { useEffect, useState } from "react";

const EmailVerifications: React.FC = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const query = new URLSearchParams(location.search);
  const dispatch = useDispatch<AppDispatch>();
  const [verificationStatus, setVerificationStatus] = useState<
    "pending" | "success" | "error"
  >("pending");
  const [message, setMessage] = useState<string>("Verifying your email...");
  const token = query.get("token");

  useEffect(() => {
    const emailVerification = async () => {
      try {
        if (token) {
          await dispatch(EmailVerification({ token })).unwrap();
            toast.success("Email verified sucessfully");
            setVerificationStatus("success");
            setMessage("Your email has been sucessfully verified");
            setTimeout(() => navigate("/"), 5000);
        } else {
          throw new Error("Invalid or Missing token");
        }
      } catch (error) {
        console.error(error);
        toast.error("Email verification failed");
        setVerificationStatus("error");
        setMessage(
          "Verification failed. The link may be invalid or expired. Register again..."
        );
        setTimeout(() => navigate("/"), 5000);
      }
    };
    emailVerification();
  }, [dispatch, token]);

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
            width: "60%",
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
          <Box
            sx={{
              fontSize: 28,
              fontWeight: 700,
              mb: 2,
              color: verificationStatus === "error" ? "red" : "#333",
            }}
          >
            {message}
          </Box>
        </Box>
      </Box>
    </>
  );
};

export default EmailVerifications;
