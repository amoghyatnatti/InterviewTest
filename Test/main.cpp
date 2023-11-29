#include <iostream>
#include <iomanip>
#include <fstream>
#include <string>
#include <sstream>
#include <map>
#include <stack>

using namespace std;

string inputFileNameStr = "expressions.txt";                  // Default location in solution folder

class OperatorMapClass
{

private:
    typedef map<char, int>    OperatorPrecedenceMapType;
    OperatorPrecedenceMapType operatorMapObj;

public:

    OperatorMapClass()
    {
        operatorMapObj.insert(OperatorPrecedenceMapType::value_type('+', 1));
        operatorMapObj.insert(OperatorPrecedenceMapType::value_type('-', 1));
        // xxx insert code here to insert * and / in the map object
        operatorMapObj.insert(OperatorPrecedenceMapType::value_type('*', 2));
        operatorMapObj.insert(OperatorPrecedenceMapType::value_type('/', 2));

    }//OperatorMapClass ()

    bool isStackOperatorHigherPrecedence(char operatorChar, char operatorStackChar)
    {
        return((operatorMapObj.count(operatorStackChar))
            &&
            (operatorMapObj.at(operatorStackChar) >= operatorMapObj.at(operatorChar)));
    }//isStackOperatorHigherPrecedence()

    bool isOperator(char token)
    {
        // xxx check if token operator Map Object is 0 or not to return true or false
        if (token == '+' || token == '-' || token == '*' || token == '/')
        {
            return true;
        }
        return false; // dummy return

    }//isOperator()

};//OperatorMapClass

OperatorMapClass  operatorMapObj;

class ShuntingYardClass
{

public:

    string createPostFixFrom(string infixString)
    {

        string       outputString;
        stack <char> operatorStackObj;

        for (char token : infixString)
        {
            switch (token)
            {
            case '/': case '*': case '+': case '-':
                // xxx insert code here
                /*
                 while (the stack is not empty
                          AND
                        the top of the stack token is not a left parenthesis
                          AND
                        precedence of the current operator <= precedence of the top of the stack token
                       )
                 {
                      Push back the stack top token to the output string
                      Pop the stack top and discard it
                  }//while-end

                   Push the current operator token onto the stack
                */

                while (!operatorStackObj.empty() && operatorStackObj.top() != '('
                    && operatorMapObj.isStackOperatorHigherPrecedence(token, operatorStackObj.top()))
                {
                    outputString.push_back(operatorStackObj.top());
                    operatorStackObj.pop();
                }//while-end

                operatorStackObj.push(token);

                break;
            case '(':                                                       // left parenthesis                   
                // xxx insert code here
                // push this token on the stack
                operatorStackObj.push('(');
                break;
            case ')':
                // xxx insert code here   // right parenthesis
                while (!operatorStackObj.empty() && operatorStackObj.top() != '(')
                {
                    outputString.push_back(operatorStackObj.top());
                    operatorStackObj.pop();
                }//while-end

                operatorStackObj.pop();

                /*
                while (the stack is not empty AND the top stack token is not a left parenthesis)
                {
                   Push back the stack top token to the output string
                }//while-end

                Pop the left parenthesis from the stack and discard it
                */
                break;
            default:                                                        // operand                                                         
                // xxx insert code here
                // push back the operand symbol to the output string
                outputString.push_back(token);
                break;
            }//switch
        }//for

        // xxx insert code here

        while (!operatorStackObj.empty())
        {
            //Push back any remaining stack tokens to the output string
            outputString.push_back(operatorStackObj.top());
            operatorStackObj.pop();
        }//while-end

        return(outputString);

    }//postfix()	

};//ShuntingYardClass



class TreeNodeClass
{

public:
    TreeNodeClass* left;
    char            value;
    TreeNodeClass* right;

};//TreeNodeClass

TreeNodeClass* BuildNewNodeObjPtrMethod(char value)
{

    // xxx new a new TreeNodeClass pointer
    TreeNodeClass* newNodePtr = new TreeNodeClass;
    // set value in new node and left and right pointers
    newNodePtr->value = value;
    newNodePtr->left = NULL;
    newNodePtr->right = NULL;

    // return new node pointer
    return newNodePtr; // dummy return
};

TreeNodeClass* ConstructBET(string postFixStr)
{
    stack<TreeNodeClass*>   parseStack;
    TreeNodeClass* newNodePtr = new TreeNodeClass;
    OperatorMapClass        OperatorMapObj;

    // xxx must develop code here
    // Process each character of the post-fix expression into token
    cout << postFixStr << endl;
    char token = ' ';
    for (int i = 0; i < postFixStr.length(); i++)
    {
        token = postFixStr[i];
        // Form a new node pointer
       
        // check if an operator
        if (OperatorMapObj.isOperator(token))
        {
            // parse stack nodes into a new subtree as children
            TreeNodeClass* operatorNode = BuildNewNodeObjPtrMethod(token);
            operatorNode->right = parseStack.top();
           // cout << operatorNode->left->value << endl;
            parseStack.pop();
            operatorNode->left = parseStack.top();
            //cout << operatorNode->right->value << endl;
            parseStack.pop();
            // Save/Add this sub tree node to the stack
            //cout << operatorNode->value << endl;
            parseStack.push(operatorNode);
        }
        else // not operator
        {
            // operand, push node onto stack 
            TreeNodeClass* newSeparateNodePtr = BuildNewNodeObjPtrMethod(token);
            parseStack.push(newSeparateNodePtr);
        }
    }

    //  Place formed root node on the stack into tree

    return parseStack.top();
}

string buildString;

void preorder(TreeNodeClass* treeNode)
{
    //xxx do pre order transversal to build string
    if (treeNode == NULL)
        return;

    buildString += treeNode->value;
    
    preorder(treeNode->left);

    preorder(treeNode->right);
}

bool areParensRequired(TreeNodeClass* treeNode, char value)
{
    OperatorMapClass operatorMapObj;
    if (operatorMapObj.isOperator(value) &&
        operatorMapObj.isOperator(treeNode->value) &&
        operatorMapObj.isStackOperatorHigherPrecedence(treeNode->value, value)) {
        buildString += '(';
        return true;
    }
    return false;
}

void inorder(TreeNodeClass* treeNode)
{
    //xxx do in order transversal to build string
    bool parensRequired = false;
    if (treeNode)
    {
        // xxx check if parens required pass arguments treeNode->left, treeNode->value
        // go left
        parensRequired = areParensRequired(treeNode->left, treeNode->value);
        inorder(treeNode->left);
        if (parensRequired)
            buildString += ')';
        // add value to build string 
        buildString += treeNode->value;
        // xxx check if parens required pass arguments treeNode->right, treeNode->value
        // go right
        

        parensRequired = areParensRequired(treeNode->right, treeNode->value);
        inorder(treeNode->right);
         // check if parens required 
            //add ) to buildString
        if (parensRequired)
            buildString += ')';

    }//if
}

void postorder(TreeNodeClass* treeNode)
{
    //xxx do post order transversal to build string
    if (treeNode == NULL)
        return;

    postorder(treeNode->left);

    postorder(treeNode->right);

    buildString += treeNode->value;
}



int main()
{

    ifstream  inputFileStreamObj(inputFileNameStr);

    if (inputFileStreamObj.fail()) {
        cout << "File could not be opened !" << endl;

        return (EXIT_FAILURE);
    }//if

    string  infixExpressionStr,
        postfixExpressionStr;

    ShuntingYardClass shuntingYardObj;

    while (inputFileStreamObj >> infixExpressionStr) {

        cout << "InFix   Expression : " << infixExpressionStr << endl;
        postfixExpressionStr = shuntingYardObj.createPostFixFrom(infixExpressionStr);
        cout << "PostFix Expression : " << postfixExpressionStr << endl << endl;

        TreeNodeClass* expressionTreeRootPtr = ConstructBET(postfixExpressionStr);

        buildString = "";  preorder(expressionTreeRootPtr);
        cout << "Tree  pre-order expression is " << endl << buildString << endl << endl;

        buildString = "";  inorder(expressionTreeRootPtr);
        cout << "Tree   in-order expression is " << endl << buildString << endl << endl;

        buildString = "";  postorder(expressionTreeRootPtr);
        cout << "Tree post-order expression is " << endl << buildString << endl << endl;

        cout << endl << endl;

    };//while

    inputFileStreamObj.close();

    cout << "Press the enter key once or twice to end." << endl;

    return EXIT_SUCCESS;

}//main()

