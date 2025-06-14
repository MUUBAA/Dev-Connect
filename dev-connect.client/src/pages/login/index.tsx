import { Box, Button, TextField, Typography } from "@mui/material";
import React, { useState, useEffect } from "react";
import "./login.css"; // Import the CSS file for flip-card styles
import backgroundImage from "../../assets/images/background-img.jpg"
import { useDispatch } from "react-redux";
import type { AppDispatch } from "../../redux/stores";
import { loginUser } from "../../redux/thunk/jwtVerify";
import { ToastContainer, toast } from "react-toastify";
import { useNavigate } from "react-router";

type GhostCursorOptions = {
  element?: HTMLElement;
};

class Particle {
  initialLifeSpan: number;
  lifeSpan: number;
  position: { x: number; y: number };
  image: HTMLImageElement;

  constructor(x: number, y: number, image: HTMLImageElement) {
    const lifeSpan = 13;
    this.initialLifeSpan = lifeSpan;
    this.lifeSpan = lifeSpan;
    this.position = { x, y };
    this.image = image;
  }

  update(context: CanvasRenderingContext2D) {
    this.lifeSpan--;
    const opacity = Math.max(this.lifeSpan / this.initialLifeSpan, 0);

    context.globalAlpha = opacity;
    context.drawImage(
      this.image,
      this.position.x,
      this.position.y
    );
  }
}

function ghostCursor(options?: GhostCursorOptions) {
  const hasWrapperEl = options && options.element;
  const element: HTMLElement = (hasWrapperEl ? options!.element! : document.body) as HTMLElement;

  let width = window.innerWidth;
  let height = window.innerHeight;
  const cursor = { x: width / 2, y: height / 2 };
  const particles: Particle[] = [];
  let canvas: HTMLCanvasElement;
  let context: CanvasRenderingContext2D;

  const baseImage = new window.Image();
  baseImage.src =
    "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB4AAAAZCAQAAACMPFaRAAAAAmJLR0QA/4ePzL8AAAAJcEhZcwAACxMAAAsTAQCanBgAAAAHdElNRQflBw4DGjvbwiYUAAABYnpUWHRSYXcgcHJvZmlsZSB0eXBlIGljYwAAOI2dVFuOgzAM/M8p9gh+mxyHBiLt/S+wDiQUVlTd7iCKNHbs8SNN36WkrwY1SNBADIIiQqJAuFG22OripCQuRKCTZp0JwEtt5ngZTkiGxs4egRQUpAyDrVbjVfgDamRtinAQC9NyKPsQ6UP/OfSDk0vX0itgSiamzrbXi6Nse7hHh6AfwNwPoBiF7eAfMPgU7ZRoaA9UeiZYzc/88uQv/uvBt0DbPHZDlSOzn3iC6anozOOhNHkrbRo1AwhbDenZybK5hYWAkXN7KGYbrahRnsZCcHhxO2+LcwpjjjG34+TQfg03Yw996eEBxEvI2LXUlo2mnnOi/G5sr/Dp+P8f6LgaJ1TWtsRjkREtLt2d4x1iETW6aL9ojHu69S+9c3hnH0IupeWC275Infe7JrXcKWaZlvadgfY9qllvSyNj7GnbXwY81lzSD3dD5+EuTNuGAAAC0klEQVQ4y43U22tcVRjG4XeSSVVEsFJPhGKNVou2VC1WpRprQuKpWg21oNCixYJeeOFFwb/BywqCBnohVBQsWCJ4KJ4QKxSr1qRNMK0xSqoxMZlJJ5mZ/a31/byYNdtYEu237tZ+H9bi3ZutP4VM1Svifp6nBaHlpvGMnbG/1h6FStJZBVUvD69SY5wdFJbjNFYvw1h8o3aVa0YqCdkeXwBgjCeX5ol2cwoAC/tGChWpLJTdGj+lMWd4DJ3PE93KYCPkx7LNaE4q6y+hbH38IvFRHv43T/Q+fkj0W7sLlXROquhjZULZxvhV4iP0/sMTvYfjiX5vW1CmWVWlaaFsTVznyu6IRxM/SRdatDZzLNEfrRPFG7K1qCJF1TviAEfZhLI7PYUY5P6cbuKbRE/aA4h1HImfZ+tdqt8UDwHwNbchu9vT9fiOjQhxPakNH7EexFo+AohHstsV+okp/iUbkG3xRjGBXQjRRRnAR+0hRAcfpDThsKw7Pws+4xZknT4IGLvTuy2Bn7FtiDUcbkZ92PqE7F4/kfNPuBlZl58i5njOx+0JxGoO5XTUHkGqCNlWH8r5h9yIrMeHeBYhenzIdiDaeRdP9Gd7XFqQqg3e7cM5H6ADWW/sNAXFDbYNcS0Hc/qL9Q0UFlSTpKrmhexB/ynn73MdKhXRvCqtiKt5q1mr/2pPDTapJNUa/FE/nfP3WO2qK8rFlRwgJDphT4+1VFVf/Oknvt3Hmo3wDu0IsYo3sbT5e9g1cT5t8AXtV+jz8Zwf5BpW8npOJ8NzU61L0CY/Xgg7/bfEI2/TTz3RqbB3plhTttyPpq6qThfCM342P73Z8HR4cfa/aJNPtITd/geLxmfCS+W2/6GSlKmqydawxydzWgovz624ANrgNU0Xw16fAvBy2HfuorrsQmiTzxbDCz7NXHilcvFytLjU5gpluiTMH7h0pS6rv9ZWa1HbkvhvgOg763eepbgAAAAldEVYdGRhdGU6Y3JlYXRlADIwMjEtMDctMTRUMDM6MjY6NTArMDA6MDCiRaWRAAAAJXRFWHRkYXRlOm1vZGlmeQAyMDIxLTA3LTE0VDAzOjI2OjUwKzAwOjAw0xgdLQAAADd0RVh0aWNjOmNvcHlyaWdodABDb3B5cmlnaHQgMTk5OSBBZG9iZSBTeXN0ZW1zIEluY29ycG9yYXRlZDFs/20AAAAgdEVYdGljYzpkZXNjcmlwdGlvbgBBZG9iZSBSR0IgKDE5OTgpsLrq9gAAAABJRU5ErkJggg==";

  function init() {
    canvas = document.createElement("canvas");
    context = canvas.getContext("2d")!;
    canvas.style.top = "0px";
    canvas.style.left = "0px";
    canvas.style.pointerEvents = "none";

    if (hasWrapperEl) {
      canvas.style.position = "absolute";
      element.appendChild(canvas);
      canvas.width = (element as HTMLElement).clientWidth;
      canvas.height = (element as HTMLElement).clientHeight;
    } else {
      canvas.style.position = "fixed";
      document.body.appendChild(canvas);
      canvas.width = width;
      canvas.height = height;
    }

    bindEvents();
    loop();
  }

  function bindEvents() {
    element.addEventListener("mousemove", onMouseMove as EventListener);
    element.addEventListener("touchmove", onTouchMove as EventListener);
    element.addEventListener("touchstart", onTouchMove as EventListener);
    window.addEventListener("resize", onWindowResize);
  }

  function onWindowResize(_: UIEvent) {
    width = window.innerWidth;
    height = window.innerHeight;

    if (hasWrapperEl) {
      canvas.width = (element as HTMLElement).clientWidth;
      canvas.height = (element as HTMLElement).clientHeight;
    } else {
      canvas.width = width;
      canvas.height = height;
    }
  }

  function onTouchMove(e: TouchEvent) {
    if (e.touches.length > 0) {
      for (let i = 0; i < e.touches.length; i++) {
        addParticle(e.touches[i].clientX, e.touches[i].clientY, baseImage);
      }
    }
  }

  function onMouseMove(e: MouseEvent) {
    if (hasWrapperEl) {
      const boundingRect = element.getBoundingClientRect();
      cursor.x = e.clientX - boundingRect.left;
      cursor.y = e.clientY - boundingRect.top;
    } else {
      cursor.x = e.clientX;
      cursor.y = e.clientY;
    }

    addParticle(cursor.x, cursor.y, baseImage);
  }

  function addParticle(x: number, y: number, image: HTMLImageElement) {
    particles.push(new Particle(x, y, image));
  }

  function updateParticles() {
    context.clearRect(0, 0, width, height);

    for (let i = 0; i < particles.length; i++) {
      particles[i].update(context);
    }

    for (let i = particles.length - 1; i >= 0; i--) {
      if (particles[i].lifeSpan < 0) {
        particles.splice(i, 1);
      }
    }
  }

  function loop() {
    updateParticles();
    requestAnimationFrame(loop);
  }

  init();
}

const Loginpage: React.FC = () => {
  const [isSignIn, setIsSIgnIn] = useState(true);
  const [activeBtn, setActiveBtn] = useState<"signin" | "signup">("signin");
  const dispatch = useDispatch<AppDispatch>();
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  useEffect(() => {
    ghostCursor();
  }, []);

  const onLoginSubmit = async () => {
    try {
      const response = await dispatch(loginUser({userName: userName, password}));
      if(response.type === "loginUser/fulfilled") {
        toast.dismiss();
        toast.success("Login Sucessfully");
        setUserName("");
        setPassword("");
        navigate("/home")
      }
      else {
        const errorMessage = response.payload as string;
        toast.error(errorMessage);
      }
    } catch {
      toast.error("login failed")
    }
  }

  return (
    <>
      <Box
        sx={{
          width: "100vw",
          height: "100vh",
          fontFamily: "Comfortaa, Comfortaa",
          color: "#ffffff",
          background: `linear-gradient(-45deg, #ee7752cc, #e73c7ecc, #23a6d5cc, #23d5abcc), url(${backgroundImage}) center/cover no-repeat`,
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
        <div className="box">
          <div
            className="flip-card-inner"
            style={{
              transform: isSignIn ? "rotateY(0deg)" : "rotateY(-180deg)",
            }}
          >
            {/* Sign In Side */}
            <div className="box-login">
              <Box
                sx={{
                  width: "100%",
                  p: 4,
                  maxWidth: 480,
                  minWidth: 340,
                  display: "flex",
                  flexDirection: "column",
                  alignItems: "center",
                }}
              >
                <Box
                  sx={{
                    display: "flex",
                    width: "100%",
                    mb: 2,
                    borderRadius: 4,
                    bgcolor: "#F2F2F2",
                  }}
                >
                  <Button
                    onClick={() => {
                      setIsSIgnIn(true);
                      setActiveBtn("signin");
                    }}
                    sx={{
                      flex: 1,
                      borderRadius: "999px",
                      backgroundColor: isSignIn ? "#fff" : "transparent",
                      boxShadow: isSignIn ? 1 : "none",
                      border:
                        activeBtn === "signin"
                          ? "2px solid #FDFAF6"
                          : "2px solid transparent",
                      transition: "all 0.3s ease-in-out",
                      "&:hover": {
                        border: "2px solid transparent",
                        backgroundColor: isSignIn ? "#fff" : "transparent",
                      },
                    }}
                  >
                    Sign In
                  </Button>
                  <Button
                    onClick={() => {
                      setIsSIgnIn(false);
                      setActiveBtn("signup");
                    }}
                    sx={{
                      flex: 1,
                      borderRadius: "999px",
                      backgroundColor: !isSignIn ? "#fff" : "transparent",
                      boxShadow: !isSignIn ? 1 : "none",
                      border:
                        activeBtn === "signup"
                          ? "2px solid #FDFAF6"
                          : "2px solid transparent",
                      transition: "all 0.3s ease-in-out",
                      "&:hover": {
                        border: "2px solid transparent",
                        backgroundColor: !isSignIn ? "#fff" : "transparent",
                      },
                    }}
                  >
                    Sign Up
                  </Button>
                </Box>
                {/* Sign In Form */}
                <TextField
                  label="Username"
                  variant="outlined"
                  fullWidth
                  margin="normal"
                  value={userName}
                  onChange={(e) => setUserName(e.target.value)}
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
                  }}
                />
                <TextField
                  label="Password"
                  variant="outlined"
                  type="password"
                  fullWidth
                  margin="normal"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  sx={{
                    background: "#fff",
                    borderRadius: 3,
                    mb: 3,
                    "& .MuiOutlinedInput-root": {
                      borderRadius: 3,
                    },
                    input: { color: "#222" },
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
                      bgcolor: "#6A9AB0",
                    },
                  }}
                   onClick={onLoginSubmit}
                >
                  Submit
                </Button>
                <Box
                  sx={{
                    display: "flex",
                    justifyContent: "space-between",
                    width: "100%",
                    mt: 1,
                    gap: 2,
                  }}
                >
                  <Typography
                    sx={{
                      textAlign: "right",
                      color: "#ffd600",
                      fontSize: 14,
                      cursor: "pointer",
                      textDecoration: "underline",
                      "&:hover": { color: "#fff" },
                    }}
                  >
                    Forgot password?
                  </Typography>
                </Box>
              </Box>
            </div>
            {/* Sign Up Side */}
            <div className="box-signup" style={{ transform: "rotateY(180deg)" }}>
              <Box
                sx={{
                  width: "100%",
                  p: 4,
                  maxWidth: 480,
                  minWidth: 340,
                  display: "flex",
                  flexDirection: "column",
                  alignItems: "center",
                }}
              >
                <Box
                  sx={{
                    display: "flex",
                    width: "100%",
                    mb: 2,
                    borderRadius: 4,
                    bgcolor: "#F2F2F2",
                  }}
                >
                  <Button
                    onClick={() => {
                      setIsSIgnIn(true);
                      setActiveBtn("signin");
                    }}
                    sx={{
                      flex: 1,
                      borderRadius: "999px",
                      backgroundColor: isSignIn ? "#fff" : "transparent",
                      boxShadow: isSignIn ? 1 : "none",
                      border:
                        activeBtn === "signin"
                          ? "2px solid #FDFAF6"
                          : "2px solid transparent",
                      transition: "all 0.3s ease-in-out",
                      "&:hover": {
                        border: "2px solid transparent",
                        backgroundColor: isSignIn ? "#fff" : "transparent",
                      },
                    }}
                  >
                    Sign In
                  </Button>
                  <Button
                    onClick={() => {
                      setIsSIgnIn(false);
                      setActiveBtn("signup");
                    }}
                    sx={{
                      flex: 1,
                      borderRadius: "999px",
                      backgroundColor: !isSignIn ? "#fff" : "transparent",
                      boxShadow: !isSignIn ? 1 : "none",
                      border:
                        activeBtn === "signup"
                          ? "2px solid #FDFAF6"
                          : "2px solid transparent",
                      transition: "all 0.3s ease-in-out",
                      "&:hover": {
                        border: "2px solid transparent",
                        backgroundColor: !isSignIn ? "#fff" : "transparent",
                      },
                    }}
                  >
                    Sign Up
                  </Button>
                </Box>
                {/* Sign Up Form */}
                <TextField
                  label="Username"
                  variant="outlined"
                  fullWidth
                  margin="normal"
                  sx={{
                    background: "#fff",
                    borderRadius: 3,
                    mb: 2,
                    color: "blue",
                    "& .MuiOutlinedInput-root": {
                      borderRadius: 3,
                    },
                    input: { color: "#222" },
                  }}
                />
                <TextField
                  label="Email"
                  variant="outlined"
                  type="email"
                  fullWidth
                  margin="normal"
                  sx={{
                    background: "#fff",
                    borderRadius: 3,
                    mb: 3,
                    "& .MuiOutlinedInput-root": {
                      borderRadius: 3,
                    },
                    input: { color: "#222" },
                  }}
                />
                <TextField
                  label="Password"
                  variant="outlined"
                  type="password"
                  fullWidth
                  margin="normal"
                  sx={{
                    background: "#fff",
                    borderRadius: 3,
                    mb: 3,
                    "& .MuiOutlinedInput-root": {
                      borderRadius: 3,
                    },
                    input: { color: "#222" },
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
                 
                >
                  Submit
                </Button>
              </Box>
            </div>
          </div>
        </div>
      </Box>
    </>
  );
};

export default Loginpage;
