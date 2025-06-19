import { Box, Typography } from "@mui/material";





const ResetRedirectPage : React.FC =  () => {
    


    return (
        <>
        <Box  sx={{
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

            <Box
                display="flex"
                flexDirection="column"
                alignItems="center"
                justifyContent="center"
                height="100%"
            >
                <Box
                    sx={{
                        background: "rgba(255,255,255,0.12)",
                        borderRadius: 3,
                        boxShadow: 3,
                        px: 5,
                        py: 6,
                        display: "flex",
                        flexDirection: "column",
                        alignItems: "center",
                        maxWidth: 400,
                    }}
                >
                    <Typography
                        variant="h4"
                        fontWeight={700}
                        gutterBottom
                        sx={{ color: "#fff", textShadow: "0 2px 8px #23d5ab99" }}
                    >
                        Check Your Email!
                    </Typography>
                    <Typography
                        variant="body1"
                        sx={{ color: "#e0f7fa", textAlign: "center", mb: 2 }}
                    >
                        A password reset link has been sent to your email address.
                        <br />
                        Please follow the instructions to reset your password.
                    </Typography>
                </Box>
            </Box>
        </Box>
        </>
    )
}

export default ResetRedirectPage;