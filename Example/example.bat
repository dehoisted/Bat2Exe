rem example.bat
: The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software. https://github.com/dehoisted/Bat2Exe
: THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

: Info
: Example batch file for converting it into an exe.
: For suggestions, contact be on telegram: https://t.me/Constex
: If you get any compilation errors, make a new issue at https://github.com/dehoisted/Bat2Exe/issues
: Huge batch files may fail.
: Note: comments can be viewed if the exe is decompiled.

@echo off
cls
title Bat2Exe Example
echo Hello
set /p text=Enter text: 
echo Text entered = %text%
pause
start https://github.com/dehoisted/Bat2Exe
echo Brought you to Bat2Exe github page.
pause
echo Bye
