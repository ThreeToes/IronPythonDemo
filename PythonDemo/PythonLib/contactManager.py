import clr

clr.AddReferenceByName("PythonDemo")

from PythonDemo.Models import Contact
from PythonDemo.Scripting import IHookListener
#Bit of a cheap thing to do, but this allows nicer looking python if we hide this behind our module
import callbackManager

class hook_listener(IHookListener):
    """
    Python wrapper for IHookListener. Not really necessary in this case, but here for an example
    """
    def Callback(self, payload):
        pass

def registerCallback(event, listener):
    """
    Register a listener
    """
    callbackManager.RegisterCallback(event, listener)