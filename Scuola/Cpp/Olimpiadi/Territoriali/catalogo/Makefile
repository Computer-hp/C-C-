# Compiler
CXX = g++

# Compiler flags
CXXFLAGS = -I./include -Wall -Wextra -std=c++17

# Sources and headers
SRC_DIR = ./src
SRC_FILES = $(wildcard $(SRC_DIR)/*.cpp)
OBJ_FILES = $(SRC_FILES:.cpp=.o)

# Executable
TARGET = my_program

# Default rule
all: $(TARGET)

# Link object files to create the executable
$(TARGET): $(OBJ_FILES)
	$(CXX) $(CXXFLAGS) -o $@ $^

# Compile source files to object files
%.o: %.cpp
	$(CXX) $(CXXFLAGS) -c $< -o $@

# Clean up generated files
clean:
	rm -f $(OBJ_FILES) $(TARGET)

.PHONY: all clean