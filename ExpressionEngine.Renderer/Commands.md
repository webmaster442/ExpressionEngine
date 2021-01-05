## Let

Defines a variable. Variable can be a number in decimal, hex or binary. Variable can also be an expression. If the expression contains only constants then it's evaluated to a number.

Examples:

```
let x 3.14
let x 0xff
let x 0b_1111_1111
let x 01247
let x 3+2
let y x*2
let z y
```

## Eval

Evaluate an expression.

Examples:

```
eval y
eval 99+33
```

## Unset

Removes a variable or all variables.

Examples:

```
unset
unset y
```


## Mode

Rerurns the current trigonometry mode or sets it. Valid values: Deg, Rad, Grad. Default is deg.

Examples: 

```
mode
mode rad
```

## Derive

Derives an expression

Examples: 

```
let expression = 2*x
derive expression x
```

## Print

Print a variable value

Examples: 

```
print x
```